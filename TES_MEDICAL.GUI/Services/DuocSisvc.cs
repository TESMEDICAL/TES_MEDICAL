using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using X.PagedList;

namespace TES_MEDICAL.GUI.Services
{
    public class DuocSisvc : IDuocSi
    {
        private readonly DataContext _context;
        public DuocSisvc(DataContext context)
        {
            _context = context;
        }

        //Hàm change STT Toa Thuốc
        public async Task<STTTOATHUOC> ChangeSoUuTien(Guid maPK)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    
                    var existingSTT = await _context.STTTOATHUOC.FindAsync(maPK);
                    existingSTT.UuTien = "C";
                    


                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return existingSTT;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        //Get All Toa Thuốc Có Trạng Thái
        public async Task<IPagedList<ToaThuoc>> SearchToaThuoc(ToaThuocSearchModel model)
        {
            IEnumerable<ToaThuoc> listUnpaged = null;
           if (model.TrangThaiPK ==1)
            {
                listUnpaged = (_context.ToaThuoc.Include(x => x.STTTOATHUOC).Include(x => x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBNNavigation).Where((delegate (ToaThuoc x)
                {
                    if ((string.IsNullOrWhiteSpace(model.KeywordSearch) || (Helper.ConvertToUnSign(x.MaPhieuKhamNavigation.MaBNNavigation.HoTen).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0) || (Helper.ConvertToUnSign(x.MaPhieuKhamNavigation.MaBNNavigation.SDT).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0)) && x.TrangThai == model.TrangThai && x.MaPhieuKhamNavigation.TrangThai == 1 && x.STTTOATHUOC != null)
                        return true;
                    else
                        return false;
                })).OrderBy(x => x.STTTOATHUOC.UuTien).ThenBy(x => x.STTTOATHUOC.STT));
            }
            else
            {
                listUnpaged = (_context.ToaThuoc.Include(x => x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBNNavigation).Where((delegate (ToaThuoc x)
                {
                    if ((string.IsNullOrWhiteSpace(model.KeywordSearch) || (Helper.ConvertToUnSign(x.MaPhieuKhamNavigation.MaBNNavigation.HoTen).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0) || (Helper.ConvertToUnSign(x.MaPhieuKhamNavigation.MaBNNavigation.SDT).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0)) && x.TrangThai == model.TrangThai && x.MaPhieuKhamNavigation.TrangThai == 2)
                        return true;
                    else
                        return false;
                })).OrderByDescending(x=>x.MaPhieuKhamNavigation.NgayKham));
            } 
                
              






            var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, 10);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }
        public async Task<IEnumerable<ChiTietToaThuoc>> GetChiTiet(Guid MaPhieu)
        {
            return await _context.ChiTietToaThuoc.Include(x => x.MaThuocNavigation)
                .Where(x=>x.MaPK == MaPhieu).ToListAsync();
        }

        public async Task<ToaThuoc> GetToaThuocByMaPhieu(Guid MaPhieu)
        {
            return await _context.ToaThuoc.Include(x => x.MaPhieuKhamNavigation).Include(x => x.MaPhieuKhamNavigation.MaBNNavigation).Include(x => x.ChiTietToaThuoc)
                .FirstOrDefaultAsync(x => x.MaPhieuKham == MaPhieu);
        }

       
        public async Task<ToaThuoc> ThanhToanThuoc(Guid maPK,string MaNV)
        {
            
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var maHD = "HD_" + DateTime.Now.ToString("ddMMyyyyhhmmss");

                    HoaDonThuoc hoadon = new HoaDonThuoc
                    {
                        MaHD = maHD,
                        MaNV = MaNV,
                        NgayHD = DateTime.Now,
                        MaPK = maPK,
                        
                        
                    };
                    await _context.HoaDonThuoc.AddAsync(hoadon);

                    var stt = await _context.STTTOATHUOC.FindAsync(maPK);
                    var existingThuoc = await _context.ToaThuoc.FindAsync(maPK);
                    existingThuoc.TrangThai = 1;
                    stt.UuTien = "B";
                    if(_context.ToaThuoc.Where(x => x.TrangThai == 1).Count() > 0)
                    {
                        stt.STT = _context.ToaThuoc.Include(x => x.STTTOATHUOC).Where(x => x.TrangThai == 1).Max(x => x.STTTOATHUOC.STT);

                    }
                    else
                    {
                        stt.STT = 1;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    var phieuKham = await _context.PhieuKham.Include(x => x.MaBNNavigation).Include(x => x.ToaThuoc).ThenInclude(x => x.HoaDonThuoc).ThenInclude(x => x.MaNVNavigation).Include(x => x.ToaThuoc.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation).FirstOrDefaultAsync(x => x.MaPK == maPK);
                    CreateHD(phieuKham);
                    return existingThuoc;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        //Ham Create Hoa Don
        public void CreateHD(PhieuKham PK)
        {

            var tongTien = PK.ToaThuoc.ChiTietToaThuoc.Sum(x => x.MaThuocNavigation.DonGia * x.SoLuong);
            var listThuoc = "";
            foreach (var item in PK.ToaThuoc.ChiTietToaThuoc)
            {

                listThuoc += $"<tr><td class='col-3'><strong>{item.MaThuocNavigation.TenThuoc}</strong></td><td class='col-4'><strong>{item.GhiChu}</strong></td><td class='col-2 text-center'><strong>{item.SoLuong}</strong></td><td class='col-3 text-end'><strong>{(item.MaThuocNavigation.DonGia * item.SoLuong).ToString("n0").Replace(',', '.')}</strong></td></tr>";

            }
            //ten | soluong | thanhtien | ghichu
            var tenNV = PK.ToaThuoc.HoaDonThuoc.MaNVNavigation.HoTen;
            var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            using (var reader = new System.IO.StreamReader(root + @"/InvoiceThuoc.html"))
            {
                string readFile = reader.ReadToEnd();
                string html = string.Empty;
                html = readFile;
                html = html.Replace("{MaHD}", PK.ToaThuoc.HoaDonThuoc.MaHD);
                html = html.Replace("{MaPK}", PK.ToaThuoc.HoaDonThuoc.MaPKNavigation.MaPhieuKham.ToString());
                html = html.Replace("{NgayHD}", PK.ToaThuoc.HoaDonThuoc.NgayHD.ToString("dd/MM/yyyy HH:mm:ss"));
                html = html.Replace("{MaNV}", tenNV);
                html = html.Replace("{TongTien}", tongTien.ToString("n0").Replace(',', '.'));
                html = html.Replace("{listThuoc}", listThuoc);
                html = html.Replace("{tenBN}", PK.MaBNNavigation.HoTen);
                html = html.Replace("{SDT}", PK.MaBNNavigation.SDT);
                html = html.Replace("{NgaySinh}", PK.MaBNNavigation.NgaySinh?.ToString("dd/MM/yyyy"));
                html = html.Replace("{DiaChi}", PK.MaBNNavigation.DiaChi);


                HtmlToPdf ohtmlToPdf = new HtmlToPdf();
                PdfDocument opdfDocument = ohtmlToPdf.ConvertHtmlString(html);
                byte[] pdf = opdfDocument.Save();
                opdfDocument.Close();

                string filePath = "";
                filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\HoaDon\HoaDonThuoc", PK.ToaThuoc.HoaDonThuoc.MaHD + ".pdf");
                System.IO.File.WriteAllBytes(filePath, pdf);

            }

        }

        public async Task<ToaThuoc> XacNhanThuocDangCho(Guid maPK)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var existingThuocDangPhat = await _context.ToaThuoc.FindAsync(maPK);
                    existingThuocDangPhat.TrangThai = 2;

                    var phieuKham = await _context.PhieuKham.FindAsync(maPK);
                    phieuKham.TrangThai = 2;
                    _context.Update(phieuKham);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return existingThuocDangPhat;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
