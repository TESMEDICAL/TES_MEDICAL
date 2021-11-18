using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using X.PagedList;

namespace TES_MEDICAL.GUI.Services
{
    public class ReportSvc : IReport
    {
        private readonly DataContext _context;
        public ReportSvc(DataContext context)
        {
            _context = context;

        }

        public async Task<HoaDon> Get(string MaHD)
        {
            var item = await _context.HoaDon.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaBSNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaBNNavigation).Include(x => x.ChiTietDV).ThenInclude(x => x.MaDVNavigation)

                .FirstOrDefaultAsync(i => i.MaHoaDon == MaHD);


            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<IEnumerable<HoaDon>> GetAllHoaDon()
        {
            return await _context.HoaDon.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaBSNavigation).ToListAsync();
        }

        public async Task<IEnumerable<HoaDonThuoc>> GetAllHoaDonThuoc()
        {
            return await _context.HoaDonThuoc.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBSNavigation).ToListAsync();
        }

        public async Task<HoaDonThuoc> GetTTHDThuoc(string MaHD)
        {
            var item = await _context.HoaDonThuoc.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x =>x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBSNavigation).Include(x =>x.MaPKNavigation).ThenInclude(x => x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBNNavigation).Include(x =>x.MaPKNavigation).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x=>x.MaThuocNavigation)

                 .FirstOrDefaultAsync(i => i.MaHD == MaHD);


            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<Response<List<ThongKeBenhViewModel>>> ThongKeBenh(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                            {
                                new SqlParameter { ParameterName = "@ngaybatdau", Value= ngayBatDau },
                                new SqlParameter { ParameterName = "@ngaykethuc", Value= ngayKetThuc },

                            };
            var result = await _context.ThongKeBenhViewModel.FromSqlRaw("EXEC dbo.ThongKeBenh @ngaybatdau,@ngaykethuc", parms.ToArray()).ToListAsync();
            return new Response<List<ThongKeBenhViewModel>> { errorCode = 0, Obj = result };
        }


        public async Task<Response<List<ThongKeDichVuViewModel>>> ThongKeDichVu(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                            {
                                new SqlParameter { ParameterName = "@ngaybatdau", Value= ngayBatDau },
                                new SqlParameter { ParameterName = "@ngaykethuc", Value= ngayKetThuc },

                            };
            var result = await _context.ThongKeViewModel.FromSqlRaw("EXEC dbo.ThongKeHDV @ngaybatdau,@ngaykethuc", parms.ToArray()).ToListAsync();
            return new Response<List<ThongKeDichVuViewModel>> { errorCode = 0, Obj  = result};
        }

        public async Task<Response<List<ThongKeDichVuViewModel>>> ThongKeHDThuoc(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                            {
                                new SqlParameter { ParameterName = "@ngaybatdau", Value= ngayBatDau },
                                new SqlParameter { ParameterName = "@ngaykethuc", Value= ngayKetThuc },

                            };
            var result = await _context.ThongKeViewModel.FromSqlRaw("EXEC dbo.ThongKeHDThuoc @ngaybatdau,@ngaykethuc", parms.ToArray()).ToListAsync();
            return new Response<List<ThongKeDichVuViewModel>> { errorCode = 0, Obj = result };
        }

        public async Task<Response<List<ThongKeSoLuongThuoc>>> ThongKeSoLuongThuoc(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                            {
                                new SqlParameter { ParameterName = "@ngaybatdau", Value= ngayBatDau },
                                new SqlParameter { ParameterName = "@ngaykethuc", Value= ngayKetThuc },

                            };
            var result = await _context.ThongKeSLThuocViewModel.FromSqlRaw("EXEC dbo.ThongKeSoLuongThuoc @ngaybatdau,@ngaykethuc", parms.ToArray()).ToListAsync();
            return new Response<List<ThongKeSoLuongThuoc>> { errorCode = 0, Obj = result };
        }

        public async Task<Response<List<ThongKeDichVuViewModel>>> ThongKeTongDoanhThu(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                            {
                                new SqlParameter { ParameterName = "@ngaybatdau", Value= ngayBatDau },
                                new SqlParameter { ParameterName = "@ngaykethuc", Value= ngayKetThuc },

                            };
            var result = await _context.ThongKeViewModel.FromSqlRaw("EXEC dbo.ThongKeTongDoanhThu @ngaybatdau,@ngaykethuc", parms.ToArray()).ToListAsync();
            return new Response<List<ThongKeDichVuViewModel>> { errorCode = 0, Obj = result };
        }

        public async Task<Response<List<ThongKeLuotKhamViewModel>>> ThongKeLuotKham(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                            {
                                new SqlParameter { ParameterName = "@ngaybatdau", Value= ngayBatDau },
                                new SqlParameter { ParameterName = "@ngaykethuc", Value= ngayKetThuc },

                            };
            var result = await _context.ThongKeLuotKhamViewModel.FromSqlRaw("EXEC dbo.ThongKeLuotKham @ngaybatdau,@ngaykethuc", parms.ToArray()).ToListAsync();
            return new Response<List<ThongKeLuotKhamViewModel>> { errorCode = 0, Obj = result };
        }
    }
}
