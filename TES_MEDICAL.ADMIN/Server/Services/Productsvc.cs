using TES_MEDICAL.ADMIN.Server.Models;
using TES_MEDICAL.ADMIN.Server.Paging;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Server.Services
{
    public interface IProduct
    {
        Task<PagedList<Product>> Get(ProductSearchModel searchModel);
        Task<Product> Get(Guid id);
        Task<Product> Add(Product model);
        Task<Product> Update(Product model);
        Task<bool> Patch(Product model);

    }
    public class Productsvc : IProduct
    {
        private readonly DataContext _context;
        public Productsvc(DataContext context)
        {
            _context = context;
        }
        public async Task<PagedList<Product>> Get(ProductSearchModel model)
        {
            IEnumerable<Product> listUnpaged;
            listUnpaged = await   _context.Product.Include(t => t.MaLoaiNavigation).ToListAsync();

            if (!string.IsNullOrWhiteSpace(model.Name))

            {
                listUnpaged = listUnpaged.Where(x => x.TenMon.ToUpper().Contains(model.Name.ToUpper()));
            }




            if (model.Gia == 1) listUnpaged = listUnpaged.Where(x => x.Gia < 100000);
            if (model.Gia == 2) listUnpaged = listUnpaged.Where(x => x.Gia >= 100000 && x.Gia <= 300000);
            if (model.Gia == 3) listUnpaged = listUnpaged.Where(x => x.Gia > 300000);
            if (model.MaLoai != Guid.Empty) listUnpaged =   listUnpaged.Where(x => x.MaLoai == model.MaLoai);
            if (model.TrangThai == false) listUnpaged = listUnpaged.Where(x => x.TrangThai == true);

            return   PagedList<Product>
                .ToPagedList(listUnpaged, model.PageNumber, model.PageSize);
        }

        public async Task<Product> Get(Guid id)
        {
            return await _context.Product.FindAsync(id);
        }
        public async Task<Product> Add(Product model)
        {
            try
            {

                model.QrURL = Path.Combine("StaticFiles", "QrCode", model.Id.ToString() + ".png");
                QRCodeGenerator _qrCode = new QRCodeGenerator();
                var detailUrl = Path.Combine(model.DetailUrl, $"DetailQr/{model.Id.ToString()}");
                QRCodeData _qrCodeData = _qrCode.CreateQrCode(detailUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(_qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                using (MemoryStream stream = new MemoryStream())

                {
                    var path = Directory.GetCurrentDirectory();
                    var save = Path.Combine(path, "StaticFiles", "QrCode", model.Id.ToString() + ".png");
                    qrCodeImage.Save(save, System.Drawing.Imaging.ImageFormat.Png);

                }
                _context.Entry(model).State = EntityState.Added;
                await _context.SaveChangesAsync();
                


                return model;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
        public async Task<Product> Update(Product model)
        {
            try
            {
                var item = await _context.Product.FindAsync(model.Id);
                item.TenMon = model.TenMon;
                item.MaLoai = model.MaLoai;
                item.Gia = model.Gia;
                item.Hinh = model.Hinh;
                item.MoTa = model.MoTa;
                item.TrangThai = model.TrangThai;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {

                return null;
            }


        }
        public async Task<bool> Patch(Product model)
        {
            try
            {
                var item = _context.Product.Find(model.Id);
                item.TrangThai = model.TrangThai;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
    }
}
