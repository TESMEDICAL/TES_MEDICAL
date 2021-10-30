using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TES_MEDICAL.CLIENTBACSI.Model;
using TES_MEDICAL.CLIENTBACSI.Services;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.CLIENTBACSI.Pages
{
    public partial class KhamBenh
    {
        [Parameter]
        public string MaPK { get; set; }
        [Inject]
        public IModal modal { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        private string _userId;
        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

        private HubConnection hubConnection;

        [Inject]

        protected AuthenticationStateProvider authProvider { get; set; }
        [Inject]
        public IConfiguration _configuration { get; set; }
        [Inject]
        IKhamBenh khambenhRep { get; set; }
        [Inject]

        public NavigationManager navigationManager { get; set; }

        private PhieuKham phieuKham { get; set; }
        private List<Thuoc> lisTThuoc { get; set; } = new List<Thuoc>();
        private List<CTToaThuocModel> cTToaThuocs { get; set; } = new List<CTToaThuocModel>();
        private List<ChiTietSinhHieu> chiTietSinhHieus { get; set; } = new List<ChiTietSinhHieu>();
      
        private DotNetObjectReference<TES_MEDICAL.CLIENTBACSI.Pages.KhamBenh> objRef { get; set; }
        protected override async Task OnInitializedAsync()
        {


            objRef = DotNetObjectReference.Create(this);
            phieuKham = await khambenhRep.GetPhieuKham(MaPK);
           
            lisTThuoc = (await khambenhRep.GetAllThuoc()).ToList();
            
        }
       

        public async Task GetListThuoc()
        {
            //await JSRuntime.InvokeVoidAsync("loadDatatableThuoc","#danhSachThuoc",lisTThuoc,objRef);
            await modal.ShowModal("exampleModal");
        }

        public void AddCtSinhHieu()
        {
            chiTietSinhHieus.Add(new ChiTietSinhHieu {MaPK = Guid.Parse(MaPK),MaSinhHieu=Guid.NewGuid() });
        }

        public async Task SendToaThuoc()
        {
            
            phieuKham.ChiTietSinhHieu = chiTietSinhHieus;
            
            var ToaThuoc = new ToaThuoc { MaPhieuKham = Guid.Parse(MaPK), TrangThai = 0 };
            foreach(var item in cTToaThuocs)
            {
                ToaThuoc.ChiTietToaThuoc.Add(new ChiTietToaThuoc { MaPK = ToaThuoc.MaPhieuKham, MaThuoc = item.Thuoc.MaThuoc, SoLuong = item.SoLuong, GhiChu = item.GhiChu });
            }    
            phieuKham.ToaThuoc = ToaThuoc;


            if(await khambenhRep.SendToaThuoc(phieuKham)!=null)
            {
                navigationManager.NavigateTo("/PhieuKham");
            }    
            else
            {
                await modal.Warning("Gửi phiếu không thành công");
            }    

        }

        [JSInvokable]
        public void AddCTToaThuoc(Guid MaThuoc)
        {
            var item = lisTThuoc.FirstOrDefault(x => x.MaThuoc == MaThuoc);
            var CTTThuoc = new CTToaThuocModel { Thuoc = item, SoLuong = 1, GhiChu = string.Empty };
            cTToaThuocs.Add(CTTThuoc);
            lisTThuoc.Remove(item);
        }

        public void RemoveThuoc(Thuoc item)
        {
            lisTThuoc.Add(item);
          
            cTToaThuocs.Remove(cTToaThuocs.FirstOrDefault(x => x.Thuoc.MaThuoc == item.MaThuoc));
        }
       
        private async Task GetClaimsPrincipalData()
        {
            var authState = (await authProvider.GetAuthenticationStateAsync()).User;
           
            
            if (authState.Identity.IsAuthenticated)
            {
              
                _claims = authState.Claims;
                _userId = authState.FindFirst(c => c.Type.Contains("nameid"))?.Value;
            }
           
        }


    }
}
