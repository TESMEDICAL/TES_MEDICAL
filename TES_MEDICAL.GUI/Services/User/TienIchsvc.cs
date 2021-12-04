﻿using Microsoft.EntityFrameworkCore;
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
