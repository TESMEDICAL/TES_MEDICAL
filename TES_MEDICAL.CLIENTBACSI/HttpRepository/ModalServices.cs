using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.CLIENTBACSI.Services
{
    public interface IModal
    {
        Task ShowModal(string id);
        Task CloseModal(string id);
        Task Success(string message);
        Task Warning(string message);
        Task<bool> Confirm(string message);


    }
    public class ModalServices:IModal
    {
        private IJSRuntime _jsRuntime;

        public ModalServices(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task ShowModal(string id)
        {
            await _jsRuntime.InvokeVoidAsync("showModal", id);
        }
        public async Task CloseModal(string id)
        {
            await _jsRuntime.InvokeVoidAsync("hideModal",id);
        }

        public async Task Success(string message)
        {
            await _jsRuntime.InvokeVoidAsync("toastr.success", message);
           
        }
        public async Task Warning(string message)
        {
            await _jsRuntime.InvokeVoidAsync("toastr.warning", message);

        }
        public async Task<bool> Confirm(string message)
        {
            return await _jsRuntime.InvokeAsync<bool>("confirm", $"{message}");
        }
    }
}
