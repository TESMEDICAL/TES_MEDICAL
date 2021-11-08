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
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.ADMIN.Client.Pages
{
    public partial class Index
    {
        protected bool loading { get; set; } = true;
        protected bool Nocontent { get; set; } = false;
        private bool isNew { get; set; } = true;
        private string inputName { get; set; }
        public List<ChuyenKhoa> ChuyenKhoas { get; set; } = new List<ChuyenKhoa>();
            public MetaData MetaData { get; set; } = new MetaData();
            public ChuyenKhoaApiSearchModel _searchmodel = new ChuyenKhoaApiSearchModel ();
            [Inject]
            public IChuyenKhoaHttpRepository ChuyenKhoaRepo { get; set; }
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

                var pagingResponse = await ChuyenKhoaRepo.GetChuyenKhoas(_searchmodel);
                ChuyenKhoas = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
                loading = false;
                Nocontent = false;
            }    

           
            
         }
        
        protected async Task GetEdit(Guid id)
        {
            isNew = false;
            input = (from item in ChuyenKhoas
                     where item.MaCK == id
                     select new Input { Name = item.TenCK, id = item.MaCK }).FirstOrDefault();
            TitleText = "Cập nhật thông tin";
            editContext = new EditContext(input);
            await modal.ShowModal("ChuyenKhoaModal");
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

                    ChuyenKhoa model = new ChuyenKhoa() { MaCK = Guid.NewGuid(), TenCK = input.Name };
                    var result = await ChuyenKhoaRepo.AddChuyenKhoa(model);
                    if (result != null)
                    {
                        await modal.Success("Thêm thành công");

                        await GetChuyenKhoa();

                    }
                    else
                    {
                        await modal.Warning("Thêm không thành công vui lòng kiểm tra lại");
                    }
                }
                else
                {
                    ChuyenKhoa model = new ChuyenKhoa() { MaCK = input.id, TenCK = input.Name };
                    var result = await ChuyenKhoaRepo.UpdateChuyenKhoa(model);
                    if (result != null)
                    {
                        await modal.Success("Cập nhật thành công");

                        await GetChuyenKhoa();

                    }
                    else
                    {
                        await modal.Warning("Cập nhật không thành công vui lòng kiểm tra lại");
                    }



                }
            }    
            
        }
        private async Task SelectedPage(int page)
        {
            _searchmodel.PageNumber = page;
            await GetChuyenKhoa();
        }
        private async Task GetChuyenKhoa()
        {


            //loading = true;
            Nocontent = false;
            _searchmodel.Name = inputName;
           
            var pagingResponse = await ChuyenKhoaRepo.GetChuyenKhoas(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                ChuyenKhoas = pagingResponse.Items;
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
            //loading = true;
            _searchmodel.Name = inputName;
            await GetChuyenKhoa();
            loading = false;
        }

        private async Task Clear()
        {
            //loading = true;
            inputName = "";
            _searchmodel = new ChuyenKhoaApiSearchModel();
            await GetChuyenKhoa();
            loading = false;
        }

        

        private async Task Delete(Guid id)
        {
            if(await modal.Confirm("Bạn muốn xóa loại thức ăn này?"))
            {
                if(await ChuyenKhoaRepo.DeleteChuyenKhoa(id))
                {
                    await modal.Success("Xóa thành công");
                    await GetChuyenKhoa();
                }
                else
                {
                    await modal.Warning("Xóa không thành công");
                }    
                
            }    
        }

    }
}
