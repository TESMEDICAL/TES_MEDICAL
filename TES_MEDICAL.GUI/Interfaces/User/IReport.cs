using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IReport
    {
        Task<IEnumerable<HoaDon>> GetAllHoaDon();
        Task<HoaDon> Get(string MaHD);

        Task<IEnumerable<HoaDonThuoc>> GetAllHoaDonThuoc();
        Task<HoaDonThuoc> GetTTHDThuoc(string MaHD);


        Task<Response<List<ThongKeDichVuViewModel>>> ThongKeDichVu(DateTime ngayBatDau, DateTime ngayKetThuc);
        Task<Response<List<ThongKeDichVuViewModel>>> ThongKeHDThuoc(DateTime ngayBatDau, DateTime ngayKetThuc);
        Task<Response<List<ThongKeDichVuViewModel>>> ThongKeTongDoanhThu(DateTime ngayBatDau, DateTime ngayKetThuc);

        Task<Response<List<ThongKeBenhViewModel>>> ThongKeBenh(DateTime ngayBatDau, DateTime ngayKetThuc);

    }
}
