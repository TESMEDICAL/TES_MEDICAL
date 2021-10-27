

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using X.PagedList;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;


namespace TES_MEDICAL.GUI.Services
{
    public class Thuocsvc : IThuoc
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public Thuocsvc(DataContext context)
        {
            _context = context;

        }






        public async Task<Response<Thuoc>> Add(Thuoc model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(model).State = EntityState.Added;
                    await _context.SaveChangesAsync();



                    await transaction.CommitAsync();
                    return new Response<Thuoc> { errorCode = 0, Obj = model };
                }








            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return new Response<Thuoc> { errorCode = -1 };
                }

                return new Response<Thuoc> { errorCode = -2 };

            }
        }

        public async Task<Thuoc> Get(Guid id)
        {

            var item = await _context.Thuoc

                .FirstOrDefaultAsync(i => i.MaThuoc == id);


            if (item == null)
            {
                return null;
            }
            return item;


        }
        public async Task<Thuoc> Edit(Thuoc model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {



                    var existingThuoc = _context.Thuoc.Find(model.MaThuoc);
                    existingThuoc.TenThuoc = model.TenThuoc;
                    existingThuoc.Vitri = model.Vitri;
                    existingThuoc.DonGia = model.DonGia;
                    existingThuoc.ThongTin = model.ThongTin;
                    existingThuoc.TrangThai = model.TrangThai;
                    existingThuoc.HinhAnh = model.HinhAnh;





                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return model;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }




        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {

                var find = await _context.Thuoc.FindAsync(Id);


                _context.Thuoc.Remove(find);
                await _context.SaveChangesAsync();

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }


        }


        public async Task<IPagedList<Thuoc>> SearchByCondition(ThuocSearchModel model)
        {

            IEnumerable<Thuoc> listUnpaged;
            listUnpaged = _context.Thuoc.OrderBy(x => x.TenThuoc);



            if (!string.IsNullOrWhiteSpace(model.TenThuocSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.TenThuoc.ToUpper().Contains(model.TenThuocSearch.ToUpper()));
            }


            if (!string.IsNullOrWhiteSpace(model.DonGiaSearch.ToString()))

            {
                listUnpaged = listUnpaged.Where(x => x.DonGia == model.DonGiaSearch);
            }




            if (!string.IsNullOrWhiteSpace(model.ThongTinSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.ThongTin.ToUpper().Contains(model.ThongTinSearch.ToUpper()));
            }







            var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }



        protected IEnumerable<Thuoc> GetAllFromDatabase()
        {
            List<Thuoc> data = new List<Thuoc>();

            data = _context.Thuoc.ToList();


            return data;

        }
    }
}


