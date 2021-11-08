using TES_MEDICAL.ADMIN.Client.Helpers;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;


using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Services
{
    public interface IPhanLoaiHttpRepository
    {
        Task<PagingResponse<PhanLoai>> GetPhanLoais(PhanLoaiSearchModel searchmodel);
        Task<PhanLoai> AddPhanLoai(PhanLoai phanLoai);
        Task<PhanLoai> UpdatePhanLoai(PhanLoai phanLoai);
        Task<bool> DeletePhanLoai(Guid id);
        

    }
    public class PhanLoaisvc:IPhanLoaiHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IAuthenticationService _service;
        private readonly IHttpService _httpService;

        public PhanLoaisvc(HttpClient client, IAuthenticationService service, IJSRuntime jSRuntime, IHttpService httpService)
        {
            _client = client;
            _service = service;
            _httpService = httpService;



        }
        public async Task<PagingResponse<PhanLoai>> GetPhanLoais(PhanLoaiSearchModel searchModel)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/Phanloai/getpage", accessToken, searchModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<PhanLoai>
            {
                Items = JsonSerializer.Deserialize<List<PhanLoai>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-PaginationPhanLoai").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };
            return pagingResponse;
        }

        public async Task<PhanLoai> AddPhanLoai(PhanLoai model)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/Phanloai", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest|| response.StatusCode ==System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();
          
            return await JsonSerializer.DeserializeAsync<PhanLoai>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
        }

        public async Task<PhanLoai> UpdatePhanLoai(PhanLoai model)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Put($"/Phanloai/{model.Id}", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PhanLoai>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<bool>  DeletePhanLoai(Guid id)
        {
            
                string accessToken = _service.User.Token;
            var response = await _httpService.Delete($"/Phanloai/{id}", accessToken,new {Id = id, TrangThai = false });
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
                var content = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
        }


    }
}
