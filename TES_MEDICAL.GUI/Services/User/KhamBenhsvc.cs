using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;
using X.PagedList;

namespace TES_MEDICAL.GUI.Services
{
    public class KhamBenhsvc : IKhamBenh
    {
        private readonly DataContext _context;
        public KhamBenhsvc(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<STTViewModel>> GetList(string MaBS)
        {
            return await (from x in _context.PhieuKham
                          join y in _context.STTPhieuKham

                          on x.MaPK equals y.MaPhieuKham
                          join bn in _context.BenhNhan
                          on x.MaBN equals bn.MaBN
                          where x.MaBS == MaBS && y.TrangThai == false&&x.TrangThai ==0
                          select new STTViewModel
                          {
                              STT = y.STT,
                              HoTen = bn.HoTen,
                              UuTien = y.MaUuTien,
                              MaPK = x.MaPK
                          }).OrderByDescending(x => x.UuTien).ThenBy(x => x.STT).ToListAsync();
        }

        public async Task<PhieuKham> GetPK(Guid MaPK)
        {
            var item = await _context.PhieuKham.Include(x => x.MaBNNavigation).ThenInclude(x => x.PhieuKham).Include(x=>x.ToaThuoc).ThenInclude(x=>x.ChiTietToaThuoc).ThenInclude(x=>x.MaThuocNavigation).FirstOrDefaultAsync(x => x.MaPK == MaPK);
            return item;
        }

        public async Task<IEnumerable<PhieuKham>>GetLichSu(Guid MaBN)
        {
            return await _context.PhieuKham.Include(x => x.MaBNNavigation).Where(x =>x.MaBN==MaBN&&x.TrangThai>=1).ToListAsync();
        }

        

        

        public async Task<PhieuKham> AddToaThuoc(PhieuKham model)
        {
            try
            {
                var uuTien = (await _context.PhieuKham.Include(x => x.STTPhieuKham).FirstOrDefaultAsync(x => x.MaPK == model.MaPK)).STTPhieuKham.MaUuTien;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    
                    if(model.ChiTietSinhHieu.Count>0)
                    {
                        foreach (var item in model.ChiTietSinhHieu)
                        {
                            item.MaSinhHieu = Guid.NewGuid();
                            _context.ChiTietSinhHieu.Add(item);
                        }
                    }    
                
                    var phieuKham = _context.PhieuKham.Find(model.MaPK);
                    phieuKham.Mach = model.Mach;
                    phieuKham.NhietDo = model.NhietDo;
                    phieuKham.HuyetAp = model.HuyetAp;
                    phieuKham.NgayTaiKham = model.NgayTaiKham;
                    phieuKham.KetQuaKham = model.KetQuaKham;
                    phieuKham.NgayTaiKham = model.NgayTaiKham;
                    phieuKham.ChanDoan = model.ChanDoan;
                    phieuKham.TrangThai = 1;
                    _context.Update(phieuKham);
                    await _context.ToaThuoc.AddAsync(model.ToaThuoc);
                    var sttoathuoc = new STTTOATHUOC { MaPK = model.MaPK, STT =_context.STTTOATHUOC.Count()>0? (_context.STTTOATHUOC.Max(x=>x.STT)+1):1, UuTien = uuTien };
                    await _context.STTTOATHUOC.AddAsync(sttoathuoc);
                    var sttpk = await _context.STTPhieuKham.FindAsync(model.MaPK);

                    sttpk.TrangThai = true;
                    _context.Update(sttpk);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return model;
                }
                
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        

        public async Task<ToaThuoc> GetToaThuoc(Guid MaPK)
        {
            return await _context.ToaThuoc.Include(x=>x.ChiTietToaThuoc).ThenInclude(x=>x.MaThuocNavigation).Include(x=>x.MaPhieuKhamNavigation).Include(x=>x.MaPhieuKhamNavigation.MaBNNavigation).ThenInclude(x=>x.PhieuKham).FirstOrDefaultAsync(x=>x.MaPhieuKham==MaPK);
        }

        public async Task<IEnumerable<Thuoc>> GetAllThuoc()
        {
            return await _context.Thuoc.Where(x => x.TrangThai).ToListAsync();
        }




        public async Task<IPagedList<PhieuKham>> SearchByCondition(PhieuKhamSearchModel model)
        {
            IEnumerable<PhieuKham> listUnpaged = null;
            if (model.TrangThai == 0)
            {
                listUnpaged = (_context.PhieuKham.Include(x => x.MaBNNavigation).Include(x => x.STTPhieuKham).Where((delegate (PhieuKham x)
                {
                    if ((string.IsNullOrWhiteSpace(model.KeywordSearch) || (Helper.ConvertToUnSign(x.MaBNNavigation.HoTen).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0) || (Helper.ConvertToUnSign(x.MaBNNavigation.SDT).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0)) && x.MaBS == model.MaBS && x.TrangThai == 0 && x.STTPhieuKham != null)
                        return true;
                    else
                        return false;
                })).OrderBy(x => x.STTPhieuKham.MaUuTien).ThenBy(x => x.STTPhieuKham.STT));

            }
            else
            {

                listUnpaged = (_context.PhieuKham.Include(x => x.MaBNNavigation).Where((delegate (PhieuKham x)
                {
                    if ((string.IsNullOrWhiteSpace(model.KeywordSearch) || (Helper.ConvertToUnSign(x.MaBNNavigation.HoTen).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0) || (Helper.ConvertToUnSign(x.MaBNNavigation.SDT).IndexOf(model.KeywordSearch, StringComparison.CurrentCultureIgnoreCase) >= 0)) && x.MaBS == model.MaBS && x.TrangThai >= 1)
                        return true;
                    else
                        return false;
                })).OrderByDescending(x => x.NgayKham));
            }








            var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, 10);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }

    }
}
