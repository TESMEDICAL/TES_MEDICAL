using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.CLIENKHAMBENH.Services
{
    public interface IKhamBenh 
    {
        Task<List<STTViewModel>> GetPhieuKham(Guid MaBS);
        
    }
    public class KhamBenhsvc : IKhamBenh
    {
        private readonly IHttpService _httpService;
        public KhamBenhsvc(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<List<STTViewModel>> GetPhieuKham(Guid MaBS)
        {
           
                
                var response = await _httpService.Get($"api/apikhambenh/GetListPK?MaBS={MaBS}",null);
                var content = await response.Content.ReadAsStreamAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await  JsonSerializer.DeserializeAsync<List<STTViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            

        }
    }
}
