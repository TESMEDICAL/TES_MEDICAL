using TES_MEDICAL.ADMIN.Client.Services;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Pages
{
    public partial class ReportPage
    {
        protected bool loading { get; set; } = true;
        protected bool Nocontent { get; set; } = false;
        protected DonHang DonHang { get; set; } = new DonHang();
        
        protected List<CartDetail> CTDH { get; set; } = new List<CartDetail>();
        private string searchPhone { get; set; }
        private byte searchTrangThai { get; set; }

      
        public List<DonHang> DonHangs { get; set; } = new List<DonHang>();
        public MetaData MetaData { get; set; } = new MetaData();
        public DonHangSearchModel _searchmodel = new DonHangSearchModel();
        [Inject]
        public IDonHangHttpRepository DonHangRepo { get; set; }
        [Inject]
        private IModal modal { get; set; }
        [Inject]
        private IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


      
      
       

        protected override async Task OnInitializedAsync()
        {

            if (authenticationService.User == null)
            {
                NavigationManager.NavigateTo("Login");

            }
            else
            {
              

                await GetDonHang();
            }



        }




      protected async Task GetDH(Guid Id)
        {
            CTDH = await DonHangRepo.GetChiTiet(Id);
            DonHang = DonHangs.FirstOrDefault(d => d.MaDH == Id);
            await modal.ShowModal("DetailCart");
        }


        protected async Task UpdateDH(byte TrangThai)
        {
            string stringcf="";
            if (TrangThai == 2) stringcf = "Xác nhận đơn hàng?";
            if (TrangThai == 3) stringcf = "Xác nhận giao hàng?";
            if (TrangThai == 4) stringcf = "Xác nhận huỷ đơn hàng?";
            if(await modal.Confirm(stringcf))
            {
                if(await DonHangRepo.UpdateDonHang(DonHang.MaDH, TrangThai)!=null)
                    {
                    await modal.Success("Cập nhật thành công");
                    DonHang.TrangThai = TrangThai;
                }
                else
                {
                    await modal.Warning("Cập nhật không thành công");
                }
                
            }    
           
           
        }
       
        private async Task SelectedPage(int page)
        {
            _searchmodel.PageNumber = page;
            await GetDonHang();
        }


        private async Task GetDonHang()
        {
            loading = true;
            Nocontent = false;
            _searchmodel.Phone = searchPhone;
            _searchmodel.TrangThai = searchTrangThai;

            var pagingResponse = await DonHangRepo.GetDonHangs(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                DonHangs = pagingResponse.Items;
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
            _searchmodel.Phone = searchPhone;
            _searchmodel.TrangThai = searchTrangThai;

            var pagingResponse = await DonHangRepo.GetDonHangs(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                DonHangs = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }

            else
            {
                await modal.Success("Không tìm thấy kết quả");
            }
            loading = false;
        }

        private async Task Clear()
        {
            loading = true;
            searchPhone = "";

            searchTrangThai = 0;
            _searchmodel = new DonHangSearchModel();
            await GetDonHang();
            loading = false;
        }



     
       

    }
}
