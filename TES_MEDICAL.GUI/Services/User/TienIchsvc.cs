﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using Microsoft.Data.SqlClient;
using TES_MEDICAL.GUI.Helpers;

namespace TES_MEDICAL.GUI.Services
{
    public class TienIchsvc : ITienIch
    {
        private readonly DataContext _context;
        private ICacheBase _cacheMemory;
        private readonly IBenh _benhService;
        private readonly IThuoc _thuocService;
        public TienIchsvc(
                           DataContext context,
                           ICacheBase caheMemory,
                           IBenh benhService,
                           IThuoc thuocService
                           )
        {
            _context = context;
            _cacheMemory = caheMemory;
            _benhService = benhService;
            _thuocService = thuocService;
            
        }
        public IEnumerable<Benh> SearchBenh(string KeyWord)
        {
            try
            {
                List<Benh> benhsCache = _cacheMemory.GetOrCreate<List<Benh>>("BENH_CACHE", TimeSpan.FromMinutes(60), _benhService.GetAllBenh);

                return benhsCache.Where(x => Helper.ConvertToUnSign(x.TenBenh).IndexOf(Helper.ConvertToUnSign(KeyWord), StringComparison.CurrentCultureIgnoreCase) >= 0).Take(10);
            }
            catch (Exception ex)
            {
                return new List<Benh>();
            }
           
          

        }
       

        public async Task<List<ChiTietToaThuoc>> GetToaThuocFill(List<string> TenBenh)
        {
            PhieuKham result = null;

            result = await (from pk in _context.PhieuKham.Include(x => x.MaBNNavigation).ThenInclude(x => x.PhieuKham).Include(x => x.ToaThuoc).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation)

                            where pk.ChanDoan.Equals(string.Join(",",TenBenh)) && pk.TrangThai >= 1 && pk.TrangThai <= 2
                            select pk).FirstOrDefaultAsync();
            if (result != null) return result.ToaThuoc.ChiTietToaThuoc.ToList() ;
           
                
            else
            {
                var listToaThuoc = new List<ChiTietToaThuoc>();
                
                foreach (var benh in TenBenh)
                {
                    var List = (from pk in _context.PhieuKham.Include(x => x.ChiTietBenh).Include(x => x.ToaThuoc).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation)



                                where !string.IsNullOrWhiteSpace(pk.ChanDoan)
                                && pk.ChanDoan == benh
                                 && pk.TrangThai >= 1 && pk.TrangThai <= 2
                                orderby pk.NgayKham descending
                                select pk).FirstOrDefault();
                    if(List!=null)
                    listToaThuoc.AddRange(List.ToaThuoc.ChiTietToaThuoc.ToList().Except(listToaThuoc));
                }
                return listToaThuoc;
            }
        }
        public IEnumerable<TrieuChung> GetTrieuChung(string TenTrieuChung)
        {
            try
            {
                List<TrieuChung> trieuchungsCache = _cacheMemory.GetOrCreate<List<TrieuChung>>("TRIEUCHUNG_CACHE", TimeSpan.FromMinutes(60), _benhService.GetAllTrieuChung);

                return trieuchungsCache.Where(x => Helper.ConvertToUnSign(x.TenTrieuChung).IndexOf(Helper.ConvertToUnSign(TenTrieuChung), StringComparison.CurrentCultureIgnoreCase) >= 0).Take(10);
            }
            catch (Exception ex)
            {
                return new List<TrieuChung>();
            }
        }

        public IEnumerable<Thuoc> GetAllThuoc()
        {
            try
            {
                return _cacheMemory.GetOrCreate<List<Thuoc>>("THUOC_CACHE", TimeSpan.FromHours(12), _thuocService.GetAllThuoc);

               
            }
            catch (Exception ex)
            {
                return new List<Thuoc>();
            }

        }
        public Thuoc GetThuoc(Guid MaThuoc)
        {
            try
            {
                List<Thuoc> thuocsCache = _cacheMemory.GetOrCreate<List<Thuoc>>("THUOC_CACHE", TimeSpan.FromMinutes(60), _thuocService.GetAllThuoc);

                return thuocsCache.FirstOrDefault(x => x.MaThuoc == MaThuoc);
            }
            catch (Exception ex)
            {
                return null;
            }
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
