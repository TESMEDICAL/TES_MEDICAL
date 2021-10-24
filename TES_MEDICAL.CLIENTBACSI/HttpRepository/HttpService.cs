

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace TES_MEDICAL.CLIENKHAMBENH.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Get(string uri, string token);
        Task<HttpResponseMessage> Post(string uri, string token, object value);

        Task<HttpResponseMessage> Put(string uri, string token, object value);
        Task<HttpResponseMessage> Delete(string uri, string token,object value);
    }

    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageServiceCookie _localStorageService;
        private IConfiguration _configuration;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageServiceCookie localStorageService,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }
        public async Task<HttpResponseMessage> Get(string uri, string token)
        {
            string accessToken = token;
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            
            if (!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.SendAsync(request);
            return response;
        }    
        public async Task<HttpResponseMessage> Post(string uri,string token,object value)
        {
            string accessToken = token;
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            if(!string.IsNullOrWhiteSpace(accessToken))
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.SendAsync(request);
            return response;

            
        }

        public async Task<HttpResponseMessage> Put(string uri, string token, object value)
        {
            string accessToken = token;
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(string uri, string token,object value)
        {
            string accessToken = token;
            var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }




        // helper methods


    }
}