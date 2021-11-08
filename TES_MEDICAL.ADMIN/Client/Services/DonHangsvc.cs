using TES_MEDICAL.ADMIN.Client.Helpers;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Services
{
    public interface IDonHangHttpRepository
    {
        Task<PagingResponse<DonHang>> GetDonHangs(DonHangSearchModel searchmodel);
        Task<List<CartDetail>> GetChiTiet(Guid id);
        Task<DonHang> UpdateDonHang(Guid id, byte TrangThai);
       

    }
    public class DonHangsvc : IDonHangHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IAuthenticationService _service;
        private readonly IHttpService _httpService;

        public DonHangsvc(HttpClient client, IAuthenticationService service, IJSRuntime jSRuntime, IHttpService httpService)
        {
            _client = client;
            _service = service;
            _httpService = httpService;



        }

        
        public async Task<PagingResponse<DonHang>> GetDonHangs(DonHangSearchModel searchModel)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/Report/getpage", accessToken, searchModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<DonHang>
            {
                Items = JsonSerializer.Deserialize<List<DonHang>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-PaginationDonHang").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };
            return pagingResponse;
        }

        public async Task<List<CartDetail>> GetChiTiet(Guid id)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Get($"Report/{id}", accessToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<CartDetail>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

       
        public async Task<DonHang> UpdateDonHang(Guid id, byte TrangThai)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Put($"/Report", accessToken, new DonHang {MaDH=id,TrangThai =TrangThai});
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<DonHang>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

    }
}
