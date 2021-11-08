using TES_MEDICAL.ADMIN.Client.Services;
using TES_MEDICAL.ADMIN.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Pages
{
    public partial class ProductDetail
    {
        [Parameter]
        public string id { get; set; }
        private Product Product { get; set; }
        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (string.IsNullOrWhiteSpace(id) || id == string.Empty)
            {
                Product = new Product();
            }

        }
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(id) || id == string.Empty)
            {
                Product = new Product();
            }
            else
            {
                Product = await ProductRepo.GetTD(new Guid(id));
            }

        }



    }
}
