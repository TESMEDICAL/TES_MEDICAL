using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Services
{
    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
        Task ShowModal(string id);
        Task CloseModal(string id);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T> GetItem<T>(string key)
        {
            //await _jsRuntime.InvokeVoidAsync("ShowAlert", new {name="Duy",age="1"  });
            var json = await _jsRuntime.InvokeAsync<string>("ReadCookie", key);

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("WriteCookie", key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("DeleteCookie", key);
        }
        public async Task ShowModal(string id)
        {
            await _jsRuntime.InvokeVoidAsync("showModal", id);
        }
        public async Task CloseModal(string id)
        {
            await _jsRuntime.InvokeVoidAsync("hideModal", id);
        }
    }
}