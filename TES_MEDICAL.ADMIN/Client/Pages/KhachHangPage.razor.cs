using TES_MEDICAL.ADMIN.Client.Services;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Pages
{
    public partial class KhachHangPage
    {
        protected bool loading { get; set; } = true;
        protected bool Nocontent { get; set; } = false;

        private KhachHang kh { get; set; } = new KhachHang();
        private string searchName { get; set; }

        private string searchPhone { get; set; }
        public List<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
        public MetaData MetaData { get; set; } = new MetaData();
        public KhachHangSearchModel _searchmodel = new KhachHangSearchModel();
        [Inject]
        public IKhachHangHttpRepository KhachHangRepo { get; set; }
        [Inject]
        private IModal modal { get; set; }
        [Inject]
        private IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


       
        protected string TitleText { get; set; }
       
        protected override async Task OnInitializedAsync()
        {

            if (authenticationService.User == null)
            {
                NavigationManager.NavigateTo("Login");

            }
            else
            {
              

                await GetKhachHang();
            }



        }

        


    
       
        private async Task SelectedPage(int page)
        {
            _searchmodel.PageNumber = page;
            await GetKhachHang();
        }


        private async Task GetKhachHang()
        {
            loading = true;
            Nocontent = false;
            _searchmodel.Name = searchName;
            _searchmodel.Phone = searchPhone;

            var pagingResponse = await KhachHangRepo.GetKhachHangs(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                KhachHangs = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
                loading = false;

            }
            else
            {
                loading = false;
                Nocontent = true;
            }



        }
        private async Task Timkiem()
        {


            loading = true;
            _searchmodel.Name = searchName;
            _searchmodel.Phone = searchPhone;

            var pagingResponse = await KhachHangRepo.GetKhachHangs(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                KhachHangs = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }

            else
            {
                await modal.Success("Không tìm thấy kết quả");
            }
            loading = false;
        }
        private async Task GetDetail(Guid id)
        {
            kh = KhachHangs.FirstOrDefault(x=>x.Id==id);
            await modal.ShowModal("KhachHangDetail");
        }

        private async Task Clear()
        {
            loading = true;
            searchName = "";

            searchPhone ="";
            _searchmodel = new KhachHangSearchModel();
            await GetKhachHang();
            loading = false;
        }



        









    }
}

