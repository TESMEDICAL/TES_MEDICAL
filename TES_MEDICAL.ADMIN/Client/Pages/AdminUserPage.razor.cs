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
    public partial class AdminUserPage
    {
        protected bool loading { get; set; } = true;
        protected bool Nocontent { get; set; } = false;

       
        private string searchName { get; set; }
       
        private bool searchTrangThai { get; set; }
        public List<AdminUser> AdminUsers { get; set; } = new List<AdminUser>();
        public MetaData MetaData { get; set; } = new MetaData();
        public AdminUserSearchModel _searchmodel = new AdminUserSearchModel();
        [Inject]
        public IAdminUserHttpRepository AdminUserRepo { get; set; }
        [Inject]
        private IModal modal { get; set; }
        [Inject]
        private IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected EditContext editContext;
        protected string TitleText { get; set; }
        public class InputAdd
        {
            public Guid id { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập UserName")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập tên Admin")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
            public string MatKhau { get; set; }
            [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
            [Compare("MatKhau",ErrorMessage ="Mật khẩu xác nhận không khớp")]
            public string MatKhauXacNhan { get; set; }
           

            public byte Quyen { get; set; }
            public bool TrangThai { get; set; }

        }
        public class InPutUpdate
        {
            public Guid id { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập UserName")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập tên Admin")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
            public byte Quyen { get; set; }
            public bool TrangThai { get; set; }
        }
        protected InputAdd inputAdd = new InputAdd();
        protected InPutUpdate inputupdate = new InPutUpdate();

       
        protected override async Task OnInitializedAsync()
        {
            
            if (authenticationService.User == null)
            {
                NavigationManager.NavigateTo("Login");

            }
            else
            {
                editContext = new EditContext(inputAdd);

                await GetAdminUser();
            }



        }

        protected async Task GetEdit(Guid id)
        {
           
            inputupdate = (from item in AdminUsers
                     where item.Id == id
                     select new InPutUpdate { Name = item.Name, id = item.Id, UserName = item.UserName, Quyen = item.Quyen, TrangThai = item.TrangThai}).FirstOrDefault();
            TitleText = "Cập nhật thông tin";
            editContext = new EditContext(inputupdate);
            await modal.ShowModal("AdminUserUpdateModal");
        }



        protected async Task ShowAddModal()
        {
           
            TitleText = "Thêm người quản trị";
            inputAdd = new InputAdd { TrangThai = true, Quyen = 1 };
            editContext = new EditContext(inputAdd);
            await modal.ShowModal("AdminUserModal");
        }
        protected async Task HandleValidSubmit()
        {
            if (editContext.Validate())
            {
                

                    AdminUser model = new AdminUser() { Id = Guid.NewGuid(), Name = inputAdd.Name, UserName = inputAdd.UserName, Pass = inputAdd.MatKhau, Quyen = inputAdd.Quyen,  TrangThai = inputAdd.TrangThai };
                    var result = await AdminUserRepo.AddAdminUser(model);
                    if (result != null)
                    {
                        await modal.Success("Thêm thành công");

                        await GetAdminUser();

                    }
                    else
                    {
                        await modal.Warning("Thêm không thành công vui lòng kiểm tra lại");
                    }
               
                   



                
            }

        }

        protected async Task UpdateHandleValidSubmit()
        {
            AdminUser model = new AdminUser() { Id = inputupdate.id, Name = inputupdate.Name, UserName = inputupdate.UserName, Quyen = inputupdate.Quyen, TrangThai = inputupdate.TrangThai };
            var result = await AdminUserRepo.UpdateAdminUser(model);
            if (result != null)
            {
                await modal.Success("Cập nhật thành công");

                await GetAdminUser();

            }
            else
            {
                await modal.Warning("Cập nhật không thành công vui lòng kiểm tra lại");
            }
        }    
        private async Task SelectedPage(int page)
        {
            _searchmodel.PageNumber = page;
            await GetAdminUser();
        }


        private async Task GetAdminUser()
        {
            loading = true;
            Nocontent = false;
            _searchmodel.Name = searchName;
            _searchmodel.TrangThai = searchTrangThai;

            var pagingResponse = await AdminUserRepo.GetAdminUsers(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                AdminUsers = pagingResponse.Items;
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
            _searchmodel.TrangThai = searchTrangThai;
           
            var pagingResponse = await AdminUserRepo.GetAdminUsers(_searchmodel);
            if (pagingResponse.Items.Count > 0)
            {
                AdminUsers = pagingResponse.Items;
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
            searchName = "";
           
            searchTrangThai = false;
            _searchmodel = new AdminUserSearchModel();
            await GetAdminUser();
            loading = false;
        }



        private async Task Delete(Guid id)
        {
            if (await modal.Confirm("Bạn muốn xóatài khoản này?"))
            {
                if (await AdminUserRepo.DeleteAdminUser(id))
                {
                    await modal.Success("Xóa thành công");
                    await GetAdminUser();
                }
                else
                {
                    await modal.Warning("Xóa không thành công");
                }

            }
        }
        private async Task Restore(Guid id)
        {
            if (await modal.Confirm("Bạn muốn khôi phục tài khoản này?"))
            {
                if (await AdminUserRepo.RestoreAdminUser(id))
                {
                    await modal.Success("Khôi phục thành công");
                    await GetAdminUser();
                }
                else
                {
                    await modal.Warning("Khôi phục thành công");
                }

            }
        }








      
    }
}

