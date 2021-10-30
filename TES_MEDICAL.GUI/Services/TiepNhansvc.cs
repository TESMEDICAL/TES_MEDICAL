
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Infrastructure;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.GUI.Services
{
    public class TiepNhanSvc : ITiepNhan
    {
        private readonly DataContext _context;
        private IHubContext<RealtimeHub> _hubContext;

        public TiepNhanSvc(DataContext context, IHubContext<RealtimeHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;

        }

        public async Task<PhieuDatLich> Edit(PhieuDatLich model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var existingLich = _context.PhieuDatLich.Find(model.MaPhieu);
                    existingLich.NgayKham = model.NgayKham;


                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return model;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


        public async Task<HoaDon> CreatePK(PhieuKhamViewModel model)
        {
            try
            {

                model.MaNVHD = "da63a519-f9fa-48ac-ab40-f1cb3c4601de";
                var maHD = "HD_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                var MaPK = Guid.NewGuid();
                var list = new List<string>();
                foreach (var item in model.dichVus)
                {
                    list.Add(item.MaDV.ToString());
                }
                var listContent = string.Join(",", list);

                AddPK(model, maHD, MaPK, listContent);




                var hd = await _context.HoaDon.Include(x => x.MaPKNavigation.MaBNNavigation).Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).Include(x => x.MaPKNavigation.STTPhieuKham).Include(x => x.MaPKNavigation.ChiTietDV).ThenInclude(x => x.MaDVNavigation).FirstOrDefaultAsync(x => x.MaHoaDon == maHD);
                Thread th_one = new Thread(() => CreateHD(hd));

                th_one.Start();
                return hd;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void AddPK(PhieuKhamViewModel model, string maHD, Guid MaPK, string listContent)
        {
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                            {
                                 new SqlParameter { ParameterName = "@MaBN", Value= model.MaBN },
                                new SqlParameter { ParameterName = "@HoTen", Value= model.HoTen },
                                new SqlParameter { ParameterName = "@SDT", Value= model.SDT },
                                new SqlParameter { ParameterName = "@NgaySinh", Value= model.NgaySinh },
                                 new SqlParameter { ParameterName = "@GioiTinh", Value= model.GioiTinh },
                                   new SqlParameter { ParameterName = "@DiaChi", Value= model.DiaChi },
                                    new SqlParameter { ParameterName = "@Email", Value= string.IsNullOrWhiteSpace(model.Email)?DBNull.Value:model.Email },
                                     new SqlParameter { ParameterName = "@MaBS", Value= model.MaBS },
                                      new SqlParameter { ParameterName = "@TrieuChung", Value=string.IsNullOrEmpty(model.TrieuChung)?DBNull.Value:model.TrieuChung },
                                       new SqlParameter { ParameterName = "@UuTien", Value= model.UuTien?"A":"B" },
                                         new SqlParameter { ParameterName = "@MaNV", Value= model.MaNVHD },
                                          new SqlParameter { ParameterName = "@MaPK", Value= MaPK },
                                           new SqlParameter { ParameterName = "@MaHD", Value= maHD },
                                            new SqlParameter { ParameterName = "@listDetail", Value= listContent }




                            };
                var result = (_context.PhieuKham.FromSqlRaw("EXEC dbo.AddPhieuKhamBN @MaBN, @HoTen,@SDT, @NgaySinh,@GioiTinh,@DiaChi,@Email,@MaBS,@TrieuChung,@UuTien,@MaNV,@MaHD,@MaPK,@listDetail", parms.ToArray()).ToList()).FirstOrDefault();
            }
            catch(Exception ex)
            {
                
            }
          
        }

        public async Task<BenhNhan> GetBN(string SDT)
        {
           return await _context.BenhNhan.FirstOrDefaultAsync(x => x.SDT == SDT);
        }


        public void CreateHD(HoaDon HD)
        {

            var tongTien = HD.MaPKNavigation.ChiTietDV.Sum(x => x.MaDVNavigation.DonGia);
            var listDichVu = "";
            foreach (var item in HD.MaPKNavigation.ChiTietDV)
            {

                listDichVu += $"<tr><td class='col-6'><strong>{item.MaDVNavigation.TenDV}</strong></td><td class='col-6 text-end'><strong>{item.MaDVNavigation.DonGia.ToString("n0").Replace(',', '.')}</strong></td></tr>";

            }
            var bn = HD.MaPKNavigation.MaBNNavigation;
            var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            using (var reader = new System.IO.StreamReader(root + @"/Invoce.html"))
            {
                string readFile = reader.ReadToEnd();
                string html = string.Empty;
                html = readFile;
                html = html.Replace("{MaHoaDon}", HD.MaPK.ToString());
                html = html.Replace("{MaNhanVien}", HD.MaNVNavigation.HoTen);
                html = html.Replace("{NgayKham}", HD.NgayHD.ToString("dd/MM/yyyy HH:mm:ss"));
                html = html.Replace("{HoTen}", bn.HoTen);
                html = html.Replace("{NgaySinh}", bn.NgaySinh?.ToString("dd/MM/yyyy"));
                html = html.Replace("{SDT}", bn.SDT);
                html = html.Replace("{DiaChi}", bn.DiaChi);
                html = html.Replace("{listDichVu}", listDichVu);
                html = html.Replace("{tongtien}", tongTien.ToString("n0").Replace(',', '.'));

                HtmlToPdf ohtmlToPdf = new HtmlToPdf();
                PdfDocument opdfDocument = ohtmlToPdf.ConvertHtmlString(html);
                byte[] pdf = opdfDocument.Save();
                opdfDocument.Close();

                string filePath = "";
                filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\HoaDon", HD.MaHoaDon + ".pdf");
                System.IO.File.WriteAllBytes(filePath, pdf);

            }

        }



        public async Task<IEnumerable<PhieuDatLich>> GetAllPhieuDatLich()
        {
            return await _context.PhieuDatLich.ToListAsync();
        }






        public async Task<PhieuDatLich> GetPhieuDatLichById(string id)
        {
            return await _context.PhieuDatLich.FindAsync(id);
        }
    }
}
