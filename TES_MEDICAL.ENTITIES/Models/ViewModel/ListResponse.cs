using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.ViewModel
{
    public partial class ListResponse
    {
        public string Result { get; set; }      
    }
    public partial class ResponseChanDoan
    {
        public Guid MaBenh { get; set; }
        public string TenBenh { get; set; }
        public int SoTrieuChung { get; set; }
        public int TongCong { get; set; }
    }
    public partial class HoaDonSearchModel
    {
        public int? Page { get; set; }
        public string KeyWord { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKT { get; set; }
        public byte Type { get; set; }
    }
    public partial class ResponseHoaDon
    {
        public string MaHD { get; set; }
        public string TenNV { get; set; }
        public string TenBS { get; set; }
        public string TenBN { get; set; }
        public DateTime NgayHD { get; set; }
        public string Type { get; set; }
    }
    public partial class ScalarInt
    {
        public int Value { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    public class PageResponse<T> where T : class
    {
        public int PageTotal { get; set; }
        public List<T> result { get; set; }
    }
}
