

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using X.PagedList;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Services
{
    public class ChuyenKhoasvc : IChuyenKhoa
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public ChuyenKhoasvc(DataContext context)
        {
            _context = context;

        }






        public async Task <ChuyenKhoa> Add(ChuyenKhoa model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(model).State = EntityState.Added;
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

        public async Task<ChuyenKhoa> Get(Guid id)
        {

            var item = await _context.ChuyenKhoa

                .FindAsync(id);


            if (item == null)
            {
                return null;
            }
            return item;


        }
        public async Task<ChuyenKhoa> Edit(ChuyenKhoa model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {



                    var existingChuyenKhoa = await _context.ChuyenKhoa.FindAsync(model.MaCK);
                    existingChuyenKhoa.TenCK = model.TenCK;





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

                var find =await _context.ChuyenKhoa.FindAsync(Id);


                _context.ChuyenKhoa.Remove(find);
                await _context.SaveChangesAsync();

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }


        }


        public async Task<IPagedList<ChuyenKhoa>> SearchByCondition(ChuyenKhoaSearchModel model)
        {

            IEnumerable<ChuyenKhoa> listUnpaged =null;
            listUnpaged = _context.ChuyenKhoa.OrderBy(x => x.TenCK);



            if (!string.IsNullOrWhiteSpace(model.TenCKSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.TenCK.ToUpper().Contains(model.TenCKSearch.ToUpper()));
            }







            var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }



        protected  IEnumerable<ChuyenKhoa> GetAllFromDatabase()
        {
            List<ChuyenKhoa> data = new List<ChuyenKhoa>();

            data = _context.ChuyenKhoa.ToList();


            return data;

        }
    }
}


