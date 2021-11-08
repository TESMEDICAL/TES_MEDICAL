using TES_MEDICAL.ADMIN.Client.Helpers;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Services
{
    public interface IProductHttpRepository
    {
        Task<PagingResponse<Product>> GetProducts(ProductSearchModel searchmodel);
        Task<Product> AddProduct(Product Product);
        Task<Product> UpdateProduct(Product Product);
        Task<bool> DeleteProduct(Guid id);
        Task<bool> RestoreProduct(Guid id);
        Task<string> UploadProductImage(MultipartFormDataContent content);
        Task<List<PhanLoai>> GetPhanLoais();
        Task<Product> GetTD(Guid id);


    }
    public class Productsvc : IProductHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IAuthenticationService _service;
        private readonly IHttpService _httpService;

        public Productsvc(HttpClient client, IAuthenticationService service, IJSRuntime jSRuntime, IHttpService httpService)
        {
            _client = client;
            _service = service;
            _httpService = httpService;



        }

        public async Task<List<PhanLoai>> GetPhanLoais()
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Get("/phanloai", accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<PhanLoai>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<PagingResponse<Product>> GetProducts(ProductSearchModel searchModel)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/Product/getpage", accessToken, searchModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<Product>
            {
                Items = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-PaginationProduct").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };
            return pagingResponse;
        }

        public async Task<Product> AddProduct(Product model)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Post("/Product", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<Product> UpdateProduct(Product model)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Put($"/Product/{model.Id}", accessToken, model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<bool> DeleteProduct(Guid id)
        {

            string accessToken = _service.User.Token;
            var response = await _httpService.Delete($"/Product", accessToken, new Product {Id = id, TrangThai=false });
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<bool> RestoreProduct(Guid id)
        {
            string accessToken = _service.User.Token;
            var response = await _httpService.Delete($"/Product", accessToken, new Product { Id = id, TrangThai = true });
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("/api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                //var imgUrl = Path.Combine(_client.BaseAddress.ToString(), postContent);
                return postContent;
            }
        }

        public async Task<Product> GetTD(Guid id)
        {
            var response = await _httpService.Get($"Customer/thucdon/{id}", "");
            var content = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
