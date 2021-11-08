

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
    public class TheLoaisvc : ITheLoai
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public TheLoaisvc(DataContext context)
        {
            _context = context;

        }






        public async Task<TheLoai> Add(TheLoai model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(model).State = EntityState.Added;
                    await _context.SaveChangesAsync();



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

        public async Task<TheLoai> Get(Guid id)
        {

            var item = await _context.TheLoai

                .FirstOrDefaultAsync(i => i.MaTL == id);


            if (item == null)
            {
                return null;
            }
            return item;


        }
        public async Task<TheLoai> Edit(TheLoai model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {



                    var existingTheLoai = _context.TheLoai.Find(model.MaTL);
                    existingTheLoai.TenTL = model.TenTL;





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

                var find = await _context.TheLoai.FindAsync(Id);


                _context.TheLoai.Remove(find);
                await _context.SaveChangesAsync();

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }


        }


        public async Task<IPagedList<TheLoai>> SearchByCondition(TheLoaiSearchModel model)
        {

            IEnumerable<TheLoai> listUnpaged;
            listUnpaged = _context.TheLoai.OrderBy(x => x.TenTL);



            if (!string.IsNullOrWhiteSpace(model.TenTLSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.TenTL.ToUpper().Contains(model.TenTLSearch.ToUpper()));
            }







            var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }



        protected IEnumerable<TheLoai> GetAllFromDatabase()
        {
            List<TheLoai> data = new List<TheLoai>();

            data = _context.TheLoai.ToList();


            return data;

        }

        public async Task<IEnumerable<TheLoai>> GetAll()
        {
            return await _context.TheLoai.ToListAsync();
        }
    }
}


