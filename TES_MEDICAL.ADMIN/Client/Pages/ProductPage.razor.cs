using TES_MEDICAL.ADMIN.Client.Services;
using TES_MEDICAL.ADMIN.Client.Shared;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Pages
{
    public partial class ProductPage
    {
        protected bool loading { get; set; } = true;
        protected bool Nocontent { get; set; } = false;

        private bool isNew { get; set; } = true;
       private string searchName { get; set; }
        private int searchGia { get; set; }
        private bool searchTrangThai { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public MetaData MetaData { get; set; } = new MetaData();
        public ProductSearchModel _searchmodel = new ProductSearchModel();
        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }
        [Inject]
        private IModal modal { get; set; }
        [Inject]
        private IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpClient _client { get; set; }
       
       
        protected EditContext editContext;
        protected string TitleText { get; set; }
        public class Input
        {
            public Guid id { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập tên")]
            public string Name { get; set; }
            
            public string Mota { get; set; }
            [Required(ErrorMessage = "Giá không được để trống")]
            [Range(0,double.MaxValue,ErrorMessage ="Giá không được nhỏ hơn 0")]
            public decimal Gia { get; set; }
           
            public string Hinh { get; set; }
            
            public Guid MaLoai { get; set; }
            public bool TrangThai { get; set; }


        }
        protected Input input = new Input();

        private List<PhanLoai> phanLoais = new List<PhanLoai>();
        protected override async Task OnInitializedAsync()
        {
            phanLoais = await ProductRepo.GetPhanLoais();
            if (authenticationService.User == null)
            {
                NavigationManager.NavigateTo("Login");

            }
            else
            {
                editContext = new EditContext(input);

               await  GetProduct();
            }



        }

        protected async Task GetEdit(Guid id)
        {
            isNew = false;
            input = (from item in Products
                     where item.Id == id
                     select new Input { Name = item.TenMon, id = item.Id,Gia = item.Gia,Hinh = item.Hinh,Mota =item.MoTa,MaLoai=item.MaLoai,TrangThai = item.TrangThai }).FirstOrDefault();
            TitleText = "Cập nhật thông tin";
            editContext = new EditContext(input);
            await modal.ShowModal("ProductModal");
        }



        protected async Task ShowAddModal(string id)
        {
            isNew = true;
            TitleText = "Thêm loại thức ăn";
            input = new Input {TrangThai = true,MaLoai = phanLoais.ElementAt(0).Id };
            editContext = new EditContext(input);
            await modal.ShowModal(id);
        }
        protected async Task HandleValidSubmit()
        {
            if (editContext.Validate())
            {
                if (isNew)
                {

                    Product model = new Product() { Id = Guid.NewGuid(), TenMon = input.Name, Gia = input.Gia, Hinh = input.Hinh, MoTa = input.Mota, MaLoai = input.MaLoai, TrangThai = input.TrangThai, DetailUrl = _client.BaseAddress.ToString() };
                    var result = await ProductRepo.AddProduct(model);
                    if (result != null)
                    {
                        await modal.Success("Thêm thành công");

                        await GetProduct();

                    }
                    else
                    {
                        await modal.Warning("Thêm không thành công vui lòng kiểm tra lại");
                    }
                }
                else
                {
                    Product model = new Product() { Id = input.id, TenMon = input.Name, Gia = input.Gia, Hinh = input.Hinh, MoTa = input.Mota,MaLoai = input.MaLoai,TrangThai = input.TrangThai };
                    var result = await ProductRepo.UpdateProduct(model);
                    if (result != null)
                    {
                        await modal.Success("Cập nhật thành công");

                        await GetProduct();

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
            await GetProduct();
        }

       
        private async Task GetProduct()
        {
            loading = true;
            Nocontent = false;
            _searchmodel.Name = searchName;
            _searchmodel.Gia = searchGia;
            _searchmodel.TrangThai = searchTrangThai;
            var pagingResponse = await ProductRepo.GetProducts(_searchmodel);
            if(pagingResponse.Items.Count>0)
            {
                Products = pagingResponse.Items;
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
            _searchmodel.Gia = searchGia;
            _searchmodel.TrangThai = searchTrangThai;
            var pagingResponse = await ProductRepo.GetProducts(_searchmodel);
            if(pagingResponse.Items.Count>0)
            {
                Products = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }    

            else
            {
              await  modal.Success("Không tìm thấy kết quả");
            }    
            loading = false;
        }

        private async Task Clear()
        {
            loading = true;
            searchName = "";
            searchGia = 0;
            searchTrangThai = false;
            _searchmodel = new ProductSearchModel();
            await GetProduct();
            loading = false;
        }



        private async Task Delete(Guid id)
        {
            if (await modal.Confirm("Bạn muốn xóa món này?"))
            {
                if (await ProductRepo.DeleteProduct(id))
                {
                    await modal.Success("Xóa thành công");
                    await GetProduct();
                }
                else
                {
                    await modal.Warning("Xóa không thành công");
                }

            }
        }
        private async Task Restore(Guid id)
        {
            if (await modal.Confirm("Bạn muốn khôi phục món này?"))
            {
                if (await ProductRepo.RestoreProduct(id))
                {
                    await modal.Success("Khôi phục thành công");
                    await GetProduct();
                }
                else
                {
                    await modal.Warning("Khôi phục thành công");
                }

            }
        }








        private void AssignImageUrl(string imgUrl) => input.Hinh = imgUrl;
    }
}
