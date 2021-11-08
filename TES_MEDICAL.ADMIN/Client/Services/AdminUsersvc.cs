using TES_MEDICAL.ADMIN.Client.Helpers;

using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Helper;
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
    public interface IAdminUserHttpRepository
    {
        Task<PagingResponse<AdminUser>> GetAdminUsers(AdminUserSearchModel searchmodel);
        Task<AdminUser> AddAdminUser(AdminUser AdminUser);
        Task<AdminUser> UpdateAdminUser(AdminUser AdminUser);
        Task<bool> DeleteAdminUser(Guid id);
        Task<bool> RestoreAdminUser(Guid id);
       
       

    }
    public class AdminUsersvc : IAdminUserHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IAuthenticationService _service;
        private readonly IHttpService _httpService;

        public AdminUsersvc(HttpClient client, IAuthenticationService service, IJSRuntime jSRuntime, IHttpService httpService)
        {
            _client = client;
            _service = service;
            _httpService = httpService;



        }

      
        public async Task<PagingResponse<AdminUser>> GetAdminUsers(AdminUserSearchModel searchModel)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/AdminUser/getpage", accessToken, searchModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<AdminUser>
            {
                Items = JsonSerializer.Deserialize<List<AdminUser>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-PaginationAdminUser").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };
            return pagingResponse;
        }

        public async Task<AdminUser> AddAdminUser(AdminUser model)
        {
           
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/AdminUser", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<AdminUser>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<AdminUser> UpdateAdminUser(AdminUser model)
        {
           
            string accessToken = _service.User.Token;
            var response = await _httpService.Put($"/AdminUser/{model.Id}", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<AdminUser>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<bool> DeleteAdminUser(Guid id)
        {

            string accessToken = _service.User.Token;
            var response = await _httpService.Delete($"/AdminUser", accessToken, new AdminUser { Id = id, TrangThai = false });
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<bool> RestoreAdminUser(Guid id)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Delete($"/AdminUser", accessToken, new AdminUser { Id = id, TrangThai = true });
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

      


    }
}
