using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OtpSharp;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Infrastructure;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;


namespace TES_MEDICAL.GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomer _service;
        private readonly IValidate _valid;
        private readonly ITinTuc _tintucService;
        private readonly IDuocSi _duocSiService;
        private readonly IDichVu _dichVuService;
        private readonly ITienIch _tienichRep;
        private OTPCLASS totp;



        private IHubContext<SignalServer> _hubContext;

        public HomeController(ILogger<HomeController> logger,
                                ICustomer service,
                                IValidate valid,
                                IHubContext<SignalServer> hubContext,
                                ITinTuc tintucService,
                                IDuocSi duocSiService,
                                IDichVu dichvuService,
                                ITienIch tienichRep,
                                OTPCLASS toptpRep
            )
        {
            _logger = logger;
            _service = service;
            _valid = valid;
            _hubContext = hubContext;
            _tintucService = tintucService;
            _duocSiService = duocSiService;
            _dichVuService = dichvuService;
            _tienichRep = tienichRep;
            totp = toptpRep;

        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid MaTL)
        {
            ViewBag.TL1 = await _tintucService.GetTinTuc(Guid.Empty);
            ViewBag.TL2 = await _tintucService.GetTinTuc(Guid.Parse("7644AC01-B920-49C1-93C5-251319BBC90E"));
            ViewBag.TL3 = await _tintucService.GetTinTuc(Guid.Parse("AB6FE512-9C64-4EEA-BC14-25A517423C58"));
            ViewBag.TL4 = await _tintucService.GetTinTuc(Guid.Parse("AB215DC0-5855-42C3-85A5-EF00A2FABE65"));
            return View(await _tintucService.GetTinTuc(MaTL));
        }

        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult DatLich()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DatLich(PhieuDatLich model)
        {
            model.MaPhieu = "PK_" + (Helper.GetUniqueKey()).ToUpper();
            if (ModelState.IsValid)
            {
                if (model.NgayKham < DateTime.Now)
                {
                    ModelState.AddModelError("NgayKham", "Ngày khám phải sau ngày hiện tại");
                    return View(model);
                }
                var result = await _service.DatLich(model);
                if (result != null)
                {
                    if (model.Email != null)
                    {
                        Thread th_one = new Thread(() => Helper.SendMail(model.Email, "[TES-MEDICAL] Xác nhận đặt lịch khám", message(model))); //SendMail

                        th_one.Start();
                        
                    }

                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", result.TenBN, result.NgaySinh?.ToString("dd/MM/yyyy"), result.SDT, result.NgayKham, result.MaPhieu);

                    return RedirectToAction("ResultDatLich", "Home", new { MaPhieu = model.MaPhieu });
                }
            }


            return View(model);

        }

        [Produces("application/json")]
        [HttpGet("searchtrieuchung")]
        [Route("api/ChanDoan/searchtrieuchung")]
        public async Task<IActionResult> SearchTrieuChung()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var trieuchung = (await _tienichRep.GetTrieuChung(term)).Select(x => x.TenTrieuChung);
                return Ok(trieuchung);
            }
            catch
            {
                return BadRequest();
            }
        }


        [Produces("application/json")]
        [HttpPost("GetTrieuChungNew")]
        [Route("api/ChanDoan/GetTrieuChungNew")]
        public IActionResult GetTrieuChungNew(string[] ListTrieuChung)
        {
            try
            {

                var trieuchungs = _tienichRep.GetListChanDoan(ListTrieuChung.ToList());
                return Ok(trieuchungs);
            }
            catch
            {
                return Ok(new List<string>());
            }
        }


        [Produces("application/json")]
        [HttpPost("KetQuaChanDoan")]
        [Route("api/ChanDoan/KetQuaChanDoan")]
        public async Task<IActionResult> KetQuaChanDoan(string[] ListTrieuChung)
        {
            try
            {
                var result = _tienichRep.KetQuaChanDoan(ListTrieuChung.ToList()).OrderBy(x => x.SoTrieuChung).ThenBy(x => x.TongCong);
                List<DataPoint> dataPoints1 = new List<DataPoint>();
                List<DataPoint> dataPoints2 = new List<DataPoint>();

                foreach (var item in result)
                {
                    dataPoints1.Add(new DataPoint((item.TenBenh + "(" + item.SoTrieuChung + "/" + item.TongCong + ")").ToString(), item.SoTrieuChung));
                }
                foreach (var item in result)
                {
                    dataPoints2.Add(new DataPoint((item.TenBenh + "(" + item.SoTrieuChung + "/" + item.TongCong + ")").ToString(), item.TongCong - item.SoTrieuChung));
                }


                return Ok(new { DataPoint1 = dataPoints1, DataPoint2 = dataPoints2 });
            }
            catch
            {
                return Ok(new List<string>());
            }
        }




        public async Task<IActionResult> ResultDatLich(string MaPhieu)
        {           
            var model = await _service.GetPhieuDat(MaPhieu);
            if (model != null)
            {
                return View(model);
            }                        
            return RedirectToAction("DatLichError", "Home");
        }

        public IActionResult DatLichError()
        {
            return View();
        }

        public IActionResult LichSuDatLich()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string message(PhieuDatLich model)
        {
            var request = HttpContext.Request;
            var _baseURL = $"{request.Scheme}://{request.Host}/Home/ResultDatLich?MaPhieu={model.MaPhieu}";
            var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            string Base64 = null;
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(model.MaPhieu, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    Base64 = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());                    
                }
            }

            using (var reader = new System.IO.StreamReader(root + @"/MailTheme/index.html"))
            {
                string readFile = reader.ReadToEnd();
                string StrContent = string.Empty;
                StrContent = readFile;
                //Assing the field values in the template
                StrContent = StrContent.Replace("{MaPhieu}", model.MaPhieu);
                StrContent = StrContent.Replace("{UrlResult}", _baseURL);
                StrContent = StrContent.Replace("{Base64QR}", $"<img src='{Base64}' alt='' style='height: 150px; width: 150px' />");

                return StrContent.ToString();
            }

        }

        /// <summary>
        /// Partial tin tức theo thể loại
        /// </summary>
        /// <param name="MaTL"></param>
        /// <returns></returns>
        public async Task<IActionResult> ListTheLoai(Guid MaTL)
        {

            return PartialView("_ListTheLoai", await _tintucService.GetTinTuc(MaTL));
        }


        public async Task<IActionResult> TinChiTiet(Guid id)
        {
            var baiViet = await _tintucService.Get(id);
            ViewBag.TL1 = await _tintucService.GetTinMin(Guid.Empty);

            ViewBag.Hinh = baiViet.Hinh;

            if (baiViet == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(baiViet);
        }
        

        public async Task<IActionResult> SearchByPhoneNumber(string SDT)
        {
            var listPhieuKham = await _service.SearchByPhoneNumber(SDT);
            if (listPhieuKham.Count() > 0)
            {

                return Json(JsonConvert.SerializeObject(listPhieuKham, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
            else
            {

                return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }

        public IActionResult LichSuKham()
        {
            return View();
        }


        public async Task<IActionResult> ChiTietLichSuKham(Guid MaPK)
        {
            ViewBag.CTLichSuDichVu = await _dichVuService.GetDichVu(MaPK);
            ViewBag.CTLichSuThuoc = await _duocSiService.GetChiTiet(MaPK);
            return PartialView("_PartialCT_LichSuKham", await _service.GetLichSuKhamById(MaPK));
        }


        public async Task<IActionResult> SearchDatLichByPhoneNumber(string SDT,string otp)
        {
            byte[] rfcKey = UTF8Encoding.ASCII.GetBytes(SDT);
            totp.Totp = new Totp(rfcKey, 120,
                                     OtpHashMode.Sha1, 6);
            if (totp.Totp.VerifyTotp(otp, out long timeStepMatched, new VerificationWindow(0, 0)))
            {
                var listPhieuDatLich = await _service.SearchDatLichByPhonenumber(SDT);
                if (listPhieuDatLich.Count() > 0)
                {

                    return Json(JsonConvert.SerializeObject(listPhieuDatLich, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
                }
                else
                {

                    return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
            }
            else
            {
                return Json(new { status = -3, title = "", text = "Mã xác thực không đúng.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            
        }


        public IActionResult ChanDoan()
        {
            return View();
        }

        //Gen OTP
        public IActionResult Generate(string SDT)
        {
            byte[] rfcKey = UTF8Encoding.ASCII.GetBytes(SDT);

            // Generating TOTP
            totp.Totp = new Totp(rfcKey, 120,
                                    OtpHashMode.Sha1, 6);
            return Ok(totp.Totp.ComputeTotp());

        }

    }
}
