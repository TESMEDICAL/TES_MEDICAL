using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using Microsoft.Data.SqlClient;

namespace TES_MEDICAL.GUI.Services
{
    public class TienIchsvc : ITienIch
    {
        private readonly DataContext _context;
        public TienIchsvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Benh>> SearchBenh(string KeyWord)
        {
           return await _context.Benh.Where(x =>
           string.IsNullOrWhiteSpace(KeyWord) ||
           EF.Functions.Collate(x.TenBenh, "SQL_Latin1_General_Cp1_CI_AI").Contains(EF.Functions.Collate(KeyWord, "SQL_Latin1_General_Cp1_CI_AI"))
           )
               .Take(10).ToListAsync();

        }
        public async Task<PhieuKham> GetAuToFill(string TenBenh)
        {
            return await (from pk in _context.PhieuKham.Include(x => x.MaBenhNavigation).Include(x => x.MaBNNavigation).ThenInclude(x => x.PhieuKham).Include(x => x.ToaThuoc).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation)
                    join b in _context.Benh
                    on pk.MaBenh equals (b.MaBenh)
                    where b.TenBenh.Equals(TenBenh)&&pk.TrangThai>=1&&pk.TrangThai<=2
                    select pk).FirstOrDefaultAsync();           
        }

        public async Task<List<ChiTietToaThuoc>> GetToaThuocFill(string TenBenh)
        {
            PhieuKham result = null;

            result = await (from pk in _context.PhieuKham.Include(x => x.MaBenhNavigation).Include(x => x.MaBNNavigation).ThenInclude(x => x.PhieuKham).Include(x => x.ToaThuoc).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation)

                            where pk.ChanDoan.Equals(TenBenh) && pk.TrangThai >= 1 && pk.TrangThai <= 2
                            select pk).FirstOrDefaultAsync();
            if (result != null) return result.ToaThuoc.ChiTietToaThuoc.ToList() ;
           
                
            else
            {
                var listToaThuoc = new List<ChiTietToaThuoc>();
                var listBenh = TenBenh.Split(",");
                foreach (var benh in listBenh)
                {
                    var List = (from pk in _context.PhieuKham.Include(x => x.ChiTietBenh).Include(x => x.ToaThuoc).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation)

                               join ctbenh in _context.ChiTietBenh.Include(x => x.MaBenhNavigation)
                                on pk.MaPK equals ctbenh.MaPK
                                where ctbenh.MaBenhNavigation.TenBenh == benh
                                && pk.ChiTietBenh.Count() == 1
                                && !string.IsNullOrWhiteSpace(pk.ChanDoan)
                                 && pk.TrangThai >= 1 && pk.TrangThai <= 2
                                orderby pk.NgayKham descending
                                select pk).FirstOrDefault();
                    listToaThuoc.Concat(List.ToaThuoc.ChiTietToaThuoc);
                }
                return listToaThuoc;
            }
        }
        public async Task<List<TrieuChung>> GetTrieuChung(string TenTrieuChung)
        {
            return await _context.TrieuChung.Where(x =>
         
          EF.Functions.Collate(x.TenTrieuChung, "SQL_Latin1_General_Cp1_CI_AI").Contains(EF.Functions.Collate(TenTrieuChung, "SQL_Latin1_General_Cp1_CI_AI"))
          )
              .OrderBy(x => x.TenTrieuChung).Take(10).ToListAsync();
        }
        public List<ListResponse> GetListChanDoan(List<string> ListTrieuChung)
        {
            try
            {
                var listContent = string.Join(",", ListTrieuChung);
                var result = _context.ListResponses.FromSqlRaw("EXEC dbo.ListrieuChungNew @listTrieuChung", new SqlParameter { ParameterName = "@listTrieuChung", Value = listContent }).ToList();
                return result;
            }
            catch 
            {
                return null;
            }           
        }
        public List<ResponseChanDoan> KetQuaChanDoan(List<string> ListTrieuChung)
        {
            try
            {
                var listContent = string.Join(",", ListTrieuChung);
                var result = _context.ResponseChanDoans.FromSqlRaw("EXEC dbo.GoiYBenh @listTrieuChung", new SqlParameter { ParameterName = "@listTrieuChung", Value = listContent }).ToList();
                return result;
            }
            catch 
            {
                return null;
            }
        }
    }
}
