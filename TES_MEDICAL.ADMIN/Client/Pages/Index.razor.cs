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
    public partial class Index
    {
        protected bool loading { get; set; } 
        protected bool Nocontent { get; set; } = false;
        private bool isNew { get; set; } = true;
        private string inputName { get; set; }
        public List<PhanLoai> phanLoais { get; set; } = new List<PhanLoai>();
            public MetaData MetaData { get; set; } = new MetaData();
            public PhanLoaiSearchModel _searchmodel = new PhanLoaiSearchModel ();
            [Inject]
            public IPhanLoaiHttpRepository PhanloaiRepo { get; set; }
            [Inject]
        private IModal modal { get; set; }
        [Inject]
        private IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected EditContext editContext;
        protected string TitleText { get; set; }
        public class Input
        {
            public Guid id { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập tên")]
            public string Name { get; set; }

        }
        protected Input input = new Input();
       

        protected override async Task OnInitializedAsync()
        {
            if(authenticationService.User==null)
            {
                NavigationManager.NavigateTo("Login");

            }   
            else
            {
                editContext = new EditContext(input);

                var pagingResponse = await PhanloaiRepo.GetPhanLoais(_searchmodel);
                phanLoais = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }    

           
            
         }
        
        protected async Task GetEdit(Guid id)
        {
            isNew = false;
            input = (from item in phanLoais
                     where item.Id == id
                     select new Input { Name = item.TenLoai, id = item.Id }).FirstOrDefault();
            TitleText = "Cập nhật thông tin";
            editContext = new EditContext(input);
            await modal.ShowModal("PhanloaiModal");
        }

      
        
        protected async Task ShowAddModal(string id)
        {
            isNew = true;
            TitleText = "Thêm loại thức ăn";
        input = new Input();
        editContext = new EditContext(input);
        await modal.ShowModal(id);
        }
        protected async Task HandleValidSubmit()
        {
            if(editContext.Validate())
            {
                if (isNew)
                {

                    PhanLoai model = new PhanLoai() { Id = Guid.NewGuid(), TenLoai = input.Name };
                    var result = await PhanloaiRepo.AddPhanLoai(model);
                    if (result != null)
                    {
                        await modal.Success("Thêm thành công");

                        await GetPhanLoai();

                    }
                    else
                    {
                        await modal.Warning("Thêm không thành công vui lòng kiểm tra lại");
                    }
                }
                else
                {
                    PhanLoai model = new PhanLoai() { Id = input.id, TenLoai = input.Name };
                    var result = await PhanloaiRepo.UpdatePhanLoai(model);
                    if (result != null)
                    {
                        await modal.Success("Cập nhật thành công");

                        await GetPhanLoai();

                    }
                    else
                    {
                        await modal.Warning("Thêm không thành công vui lòng kiểm tra lại");
                    }



                }
            }    
            
        }
        private async Task SelectedPage(int page)
        {
            _searchmodel.PageNumber = page;
            await GetPhanLoai();
        }
        private async Task GetPhanLoai()
        {


            loading = true;
            Nocontent = false;
            _searchmodel.Name = inputName;
           
            var pagingResponse = await PhanloaiRepo.GetPhanLoais(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                phanLoais = pagingResponse.Items;
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
            _searchmodel.Name = inputName;
            await GetPhanLoai();
            loading = false;
        }

        private async Task Clear()
        {
            loading = true;
            inputName = "";
            _searchmodel = new PhanLoaiSearchModel();
            await GetPhanLoai();
            loading = false;
        }

        

        private async Task Delete(Guid id)
        {
            if(await modal.Confirm("Bạn muốn xóa loại thức ăn này?"))
            {
                if(await PhanloaiRepo.DeletePhanLoai(id))
                {
                    await modal.Success("Xóa thành công");
                    await GetPhanLoai();
                }
                else
                {
                    await modal.Warning("Xóa không thành công");
                }    
                
            }    
        }

    }
}
