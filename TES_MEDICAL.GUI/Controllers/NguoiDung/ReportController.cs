using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    public class ReportController : Controller
    {
        private IHostingEnvironment Environment;
        private readonly IReport _service;
        
        public ReportController(IHostingEnvironment _environment, IReport service)
        {
            Environment = _environment;
            _service = service;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReportBenhNhan()
        {
            return View();
        }

        public IActionResult ReportBenh()
        {
            return View();
        }



        //Xem và tải hoá đơn dịch vụ
        public async Task<IActionResult> ViewHoaDon()
        {

            return View(await _service.GetAllHoaDon());

        }

        [HttpGet]
        public async Task<IActionResult> Detail(string MaHD)
        {
            if (await _service.Get(MaHD) == null)
            {
                return NotFound(); ;
            }
            else
            {


                return PartialView("_partialDetail", await _service.Get(MaHD));
            }
        }



        


        //Xem và tải hoá đơn thuốc
        [HttpGet]
        public async Task<IActionResult> ViewHoaDonThuoc()
        {

            //string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "HoaDon/HoaDonThuoc/"));
            //List<FileModel> files = new List<FileModel>();

            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            //}

            return View(await _service.GetAllHoaDonThuoc());
        }

        [HttpGet]
        public async Task<IActionResult> DetailThuoc(string MaHD)
        {
            if (await _service.GetTTHDThuoc(MaHD) == null)
            {
                return NotFound();
            }
            else
            {
                return PartialView("_partialDetailThuoc", await _service.GetTTHDThuoc(MaHD));
            }
        }




        //public FileResult DownloadFile(string fileName)
        //{
        //    //Build the File Path.
        //    string path = Path.Combine(this.Environment.WebRootPath, "HoaDon/") + fileName;

        //    //Read the File data into Byte Array.
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    //Send the File to Download.
        //    return File(bytes, "application/octet-stream", fileName);
        //}

        //public FileResult DownloadFile1(string fileName)
        //{
        //    //Build the File Path.
        //    string path = Path.Combine(this.Environment.WebRootPath, "HoaDon/HoaDonThuoc/") + fileName;

        //    //Read the File data into Byte Array.
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    //Send the File to Download.
        //    return File(bytes, "application/octet-stream", fileName);
        //}

        public async Task<IActionResult> ThongKeDichVu(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            return View();
        }

        public async Task<IActionResult> ThongKeDichVuAction(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }
            var listmodel = await _service.ThongKeDichVu((DateTime)ngayBatDau, (DateTime)ngayKetThuc);
            if (listmodel.errorCode == 0)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var item in (await _service.ThongKeDichVu((DateTime)ngayBatDau, (DateTime)ngayKetThuc)).Obj)
                {
                    dataPoints.Add(new DataPoint("Tháng " + item.Thang, (decimal)item.TongTien));
                }
                return Ok(new { dataPoints = dataPoints , dataTable = listmodel});
                
                
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> ThongKeHDThuoc(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }
            var listmodel = await _service.ThongKeHDThuoc((DateTime)ngayBatDau, (DateTime)ngayKetThuc);
            if (listmodel.errorCode == 0)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var item in (await _service.ThongKeHDThuoc((DateTime)ngayBatDau, (DateTime)ngayKetThuc)).Obj)
                {
                    dataPoints.Add(new DataPoint("Tháng " + item.Thang, (decimal)item.TongTien));
                }
                return Ok(new { dataPoints = dataPoints, dataTable = listmodel });


            }
            else
            {
                return BadRequest();
            }
        }


        public async Task<IActionResult> ThongKeTongDoanhThu(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }
            var listmodel = await _service.ThongKeTongDoanhThu((DateTime)ngayBatDau, (DateTime)ngayKetThuc);
            if (listmodel.errorCode == 0)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var item in (await _service.ThongKeTongDoanhThu((DateTime)ngayBatDau, (DateTime)ngayKetThuc)).Obj)
                {
                    dataPoints.Add(new DataPoint("Tháng " + item.Thang, (decimal)item.TongTien));
                }
                return Ok(new { dataPoints = dataPoints, dataTable = listmodel });


            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> ThongKeBenh(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if(ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }
            var listmodel = await _service.ThongKeBenh((DateTime)ngayBatDau, (DateTime)ngayKetThuc);
            if (listmodel.errorCode == 0)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var item in (await _service.ThongKeBenh((DateTime)ngayBatDau, (DateTime)ngayKetThuc)).Obj)
                {
                    dataPoints.Add(new DataPoint(item.tenBenh, item.soLuong));
                }
                return Ok(new { dataPoints = dataPoints, dataTable = listmodel });


            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> ThongKeSoLuongThuoc(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }
            var listmodel = await _service.ThongKeSoLuongThuoc((DateTime)ngayBatDau, (DateTime)ngayKetThuc);
            if (listmodel.errorCode == 0)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var item in (await _service.ThongKeSoLuongThuoc((DateTime)ngayBatDau, (DateTime)ngayKetThuc)).Obj)
                {
                    dataPoints.Add(new DataPoint(item.tenThuoc, item.soLuong));
                }
                return Ok(new { dataPoints = dataPoints, dataTable = listmodel });
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> ThongKeLuotKham(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }

            var listmodel = await _service.ThongKeLuotKham((DateTime)ngayBatDau, (DateTime)ngayKetThuc);
            if (listmodel.errorCode == 0)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var item in (await _service.ThongKeLuotKham((DateTime)ngayBatDau, (DateTime)ngayKetThuc)).Obj)
                {
                    dataPoints.Add(new DataPoint("Tháng " + item.luotKham.ToString(), item.thang));
                }
                return Ok(new { dataPoints = dataPoints, dataTable = listmodel });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("LoadPagenation")]
        public IActionResult LoadPagenation(int? currentPage, int PageTotal,DateTime? NgayBD,DateTime? NgayKT,byte Type)
        {
            if (NgayBD == null && NgayKT == null)
            {
                NgayBD = DateTime.Now.AddMonths(-4);
                NgayKT = DateTime.Now;
            }

            ViewBag.currentPage = currentPage ?? 1;

            ViewBag.countPages = PageTotal;
            ViewBag.NgayBD = NgayBD;
            ViewBag.NgayKT = NgayKT;
            ViewBag.Type = Type;

            return PartialView("_Paging");
        }
        [HttpGet("PageList")]

        public IActionResult PageList(int? Page,string KeyWord,DateTime? NgayBatDau,DateTime? NgayKT,byte Type)
        {

            if (NgayBatDau == null && NgayKT == null)
            {
              NgayBatDau = DateTime.Now.AddMonths(-4);
              NgayKT = DateTime.Now;
            }

            ViewBag.currentPage = Page ?? 1;    // trang hiện tại

            var model = new HoaDonSearchModel { Page = Page, NgayBatDau = NgayBatDau, NgayKT = NgayKT, KeyWord = KeyWord, Type = Type };
           
            var listPaged = _service.SearchHDByCondition(model);



            return Ok(listPaged);



        }


    }
}
