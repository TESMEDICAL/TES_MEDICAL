using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.CLIENTBACSI.Services
{
    public interface IKhamBenh 
    {
        Task<List<STTViewModel>> GetListPhieuKham(string MaBS);
        Task<PhieuKham> GetPhieuKham(string MaPK);
        Task<IEnumerable<Thuoc>> GetAllThuoc();
        Task<PhieuKham> SendToaThuoc(PhieuKham phieuKham);
        
    }
    public class KhamBenhsvc : IKhamBenh
    {
        private readonly IHttpService _httpService;
        public KhamBenhsvc(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<List<STTViewModel>> GetListPhieuKham(string MaBS)
        {
           
                
                var response = await _httpService.Get($"apikhambenh/GetListPK?MaBS={MaBS}",null);
                var content = await response.Content.ReadAsStreamAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await  JsonSerializer.DeserializeAsync<List<STTViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            

        }
        public async Task<PhieuKham>GetPhieuKham(string MaPK)
        {
            var response = await _httpService.Get($"apikhambenh/GetPK?MaPK={MaPK}", null);
            var content = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<PhieuKham>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<IEnumerable<Thuoc>> GetAllThuoc()
        {
            var response = await _httpService.Get($"apikhambenh/GetAllThuoc", null);
            var content = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<IEnumerable<Thuoc>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<PhieuKham> SendToaThuoc(PhieuKham phieuKham)
        {
            var response = await _httpService.Post($"apikhambenh/ThemToa",null, phieuKham);
            var content = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<PhieuKham>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }


    }
}
