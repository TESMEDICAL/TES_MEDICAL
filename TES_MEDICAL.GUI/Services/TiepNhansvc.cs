using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;

namespace TES_MEDICAL.GUI.Services
{
    public class TiepNhanSvc : ITiepNhan
    {
        private readonly DataContext _context;
        public TiepNhanSvc(DataContext context)
        {
            _context = context;
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


        public async Task<PhieuKham> CreatePK(PhieuKhamViewModel model)
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
                        tongtien += item.DonGia;
                    }
                    
                    //Xuất hóa đơn dịch vụ
                    var HoaDon = new HoaDon { MaHoaDon = "HD_"+DateTime.Now.ToString("ddMMyyyyhhmmss"), MaPK = phieuKham.MaPK, NgayHD = DateTime.Now, MaNV = "6b0d19a9-fe51-458b-a4a6-9841887b60ca",TongTien = tongtien };
                    await _context.HoaDon.AddAsync(HoaDon);



                    //Add STT phiếu khám
                    var STT = new STTPhieuKham { MaPhieuKham = phieuKham.MaPK, STT = _context.STTPhieuKham.Count() + 1 };
                    if (model.UuTien) STT.MaUuTien = "A";
                    else STT.MaUuTien = "B";
                    await _context.AddAsync(STT);
                    await _context.SaveChangesAsync();


                    //Commit transaction
                    await transaction.CommitAsync();
                    return phieuKham;
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
