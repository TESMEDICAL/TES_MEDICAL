using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL_DOCTORCLIENT.Services
{
    public interface IKhamBenh 
    {
        Task<List<PhieuKham>> GetPhieuKham(Guid MaBS);
        
    }
    public class KhamBenhsvc : IKhamBenh
    {
        private readonly IHttpService _httpService;
        public KhamBenhsvc(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<List<PhieuKham>> GetPhieuKham(Guid MaBS)
        {
           
                
                var response = await _httpService.Get($"api/apikhambenh/GetPK?GetListPK={MaBS}",null);
                var content = await response.Content.ReadAsStreamAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await JsonSerializer.DeserializeAsync<List<PhieuKham>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            

        }
    }
}
