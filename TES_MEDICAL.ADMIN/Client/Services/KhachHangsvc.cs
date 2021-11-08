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
    public interface IKhachHangHttpRepository
    {
        Task<PagingResponse<KhachHang>> GetKhachHangs(KhachHangSearchModel searchmodel);
       
      


    }
    public class KhachHangsvc : IKhachHangHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IAuthenticationService _service;
        private readonly IHttpService _httpService;

        public KhachHangsvc(HttpClient client, IAuthenticationService service, IJSRuntime jSRuntime, IHttpService httpService)
        {
            _client = client;
            _service = service;
            _httpService = httpService;



        }


        public async Task<PagingResponse<KhachHang>> GetKhachHangs(KhachHangSearchModel searchModel)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/KhachHang/getpage", accessToken, searchModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<KhachHang>
            {
                Items = JsonSerializer.Deserialize<List<KhachHang>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-PaginationKhachHang").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };
            return pagingResponse;
        }

       

    }
}
