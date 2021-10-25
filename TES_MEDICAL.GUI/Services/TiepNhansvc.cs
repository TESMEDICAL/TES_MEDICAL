
﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


        public async Task<STTViewModel> CreatePK(PhieuKhamViewModel model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    decimal tongtien =0;
                    //Thêm bệnh nhân
                    var benhNhan = new BenhNhan { MaBN = Guid.NewGuid(), HoTen = model.HoTen, SDT = model.SDT, NgaySinh = model.NgaySinh, DiaChi = model.DiaChi, GioiTinh = model.GioiTinh };
                    await _context.BenhNhan.AddAsync(benhNhan);
                  // Thêm phiếu khám với thông tin bệnh nhân
                    var phieuKham = new PhieuKham {MaPK=Guid.NewGuid(), MaBN = benhNhan.MaBN, MaBS = model.MaBS, NgayKham = DateTime.Now, TrieuChungSoBo = model.TrieuChung };
                    await _context.PhieuKham.AddAsync(phieuKham);
                 
                    //Thêm chi tiết dịch vụ phiếu khám
                    foreach(var item in model.dichVus)
                    {
                        var ctdv = new ChiTietDV { MaDV = item.MaDV, MaPhieuKham = phieuKham.MaPK };
                        await _context.ChiTietDV.AddAsync(ctdv);
                        tongtien += (await _context.DichVu.FindAsync(item.MaDV)).DonGia;

                    }
                    
                    //Xuất hóa đơn dịch vụ
                    var HoaDon = new HoaDon { MaHoaDon = "HD_"+DateTime.Now.ToString("ddMMyyyyhhmmss"), MaPK = phieuKham.MaPK, NgayHD = DateTime.Now, MaNV = "da63a519-f9fa-48ac-ab40-f1cb3c4601de", TongTien = tongtien };
                    await _context.HoaDon.AddAsync(HoaDon);



                    //Add STT phiếu khám
                    var STT = new STTPhieuKham { MaPhieuKham = phieuKham.MaPK, STT = _context.STTPhieuKham.Count() + 1 };
                    if (model.UuTien) STT.MaUuTien = "A";
                    else STT.MaUuTien = "B";
                    await _context.AddAsync(STT);
                    await _context.SaveChangesAsync();


                    //Commit transaction
                    await transaction.CommitAsync();

                  
                   

                    
           


                    //In hóa đơn
                    var listDichVu = "";
                    foreach (var item in model.dichVus)
                    {
                        var Dv = await  _context.DichVu.FirstOrDefaultAsync(x => x.MaDV == item.MaDV);
                        listDichVu += $"<tr><td class='col-6'><strong>{Dv.TenDV}</strong></td><td class='col-6 text-end'><strong>{Dv.DonGia.ToString("n0").Replace(',', '.')}</strong></td></tr>";
                    }
                    var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
                    using (var reader = new System.IO.StreamReader(root + @"/Invoce.html"))
                    {
                        string readFile = reader.ReadToEnd();
                        string html = string.Empty;
                        html = readFile;
                        html = html.Replace("{MaHoaDon}", HoaDon.MaHoaDon);
                        html = html.Replace("{MaNhanVien}", (await _context.NhanVienYte.FindAsync(HoaDon.MaNV)).HoTen);
                        html = html.Replace("{NgayKham}", HoaDon.NgayHD.ToString("dd/MM/yyyy HH:mm:ss"));
                        html = html.Replace("{HoTen}", benhNhan.HoTen);
                        html = html.Replace("{NgaySinh}", benhNhan.NgaySinh?.ToString("dd/MM/yyyy"));
                        html = html.Replace("{SDT}", benhNhan.SDT);
                        html = html.Replace("{DiaChi}", benhNhan.DiaChi);
                        html = html.Replace("{listDichVu}", listDichVu);
                        html = html.Replace("{tongtien}", HoaDon.TongTien?.ToString("n0").Replace(',', '.'));

                        HtmlToPdf ohtmlToPdf = new HtmlToPdf();
                        PdfDocument opdfDocument = ohtmlToPdf.ConvertHtmlString(html);
                        byte[] pdf = opdfDocument.Save();
                        opdfDocument.Close();

                        string filePath = "";
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\HoaDon",HoaDon.MaHoaDon+".pdf");
                        System.IO.File.WriteAllBytes(filePath, pdf);

                    }
                            return new STTViewModel {STT = STT.STT,HoTen = benhNhan.HoTen,MaPK = phieuKham.MaPK,UuTien = STT.MaUuTien };
                }
             } 
            catch(Exception ex)
            {
                return null;
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
