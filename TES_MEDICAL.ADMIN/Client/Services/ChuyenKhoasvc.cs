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
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.ADMIN.Client.Services
{
    public interface IChuyenKhoaHttpRepository
    {
        Task<PagingResponse<ChuyenKhoa>> GetChuyenKhoas(ChuyenKhoaApiSearchModel searchmodel);
        Task<ChuyenKhoa> AddChuyenKhoa(ChuyenKhoa ChuyenKhoa);
        Task<ChuyenKhoa> UpdateChuyenKhoa(ChuyenKhoa ChuyenKhoa);
        Task<bool> DeleteChuyenKhoa(Guid id);
        

    }
    public class ChuyenKhoasvc:IChuyenKhoaHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IAuthenticationService _service;
        private readonly IHttpService _httpService;

        public ChuyenKhoasvc(HttpClient client, IAuthenticationService service, IJSRuntime jSRuntime, IHttpService httpService)
        {
            _client = client;
            _service = service;
            _httpService = httpService;



        }
        public async Task<PagingResponse<ChuyenKhoa>> GetChuyenKhoas(ChuyenKhoaApiSearchModel searchModel)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/ChuyenKhoa/getpage", accessToken, searchModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<ChuyenKhoa>
            {
                Items = JsonSerializer.Deserialize<List<ChuyenKhoa>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-PaginationChuyenKhoa").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };
            return pagingResponse;
        }

        public async Task<ChuyenKhoa> AddChuyenKhoa(ChuyenKhoa model)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/ChuyenKhoa", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest|| response.StatusCode ==System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();
          
            return await JsonSerializer.DeserializeAsync<ChuyenKhoa>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
        }

        public async Task<ChuyenKhoa> UpdateChuyenKhoa(ChuyenKhoa model)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Put($"/ChuyenKhoa/{model.MaCK}", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<ChuyenKhoa>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<bool>  DeleteChuyenKhoa(Guid id)
        {
            
                string accessToken = _service.User.Token;
            var response = await _httpService.Delete($"/ChuyenKhoa/{id}", accessToken,new {Id = id, TrangThai = false });
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
                var content = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
        }


    }
}
