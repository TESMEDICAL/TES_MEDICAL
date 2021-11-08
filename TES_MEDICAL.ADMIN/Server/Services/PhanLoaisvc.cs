using TES_MEDICAL.ADMIN.Server.Models;
using TES_MEDICAL.ADMIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TES_MEDICAL.ADMIN.Server.Paging;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using TES_MEDICAL.ADMIN.Shared.Models;

namespace TES_MEDICAL.ADMIN.Server.Services
{
    public interface IPhanLoai
    {
        Task<IEnumerable<PhanLoai>> Get();
       Task <PagedList<PhanLoai>> Get(PhanLoaiSearchModel searchModel);
        Task<PhanLoai> Get(Guid id);
        Task<PhanLoai> Add(PhanLoai model);
        Task<PhanLoai> Update(PhanLoai model);
        Task<bool> Delete(Guid id);

    }
    public class PhanLoaisvc:IPhanLoai
    {
        private readonly DataContext _context;
        public PhanLoaisvc(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhanLoai>> Get()
        {
            return await _context.PhanLoai.ToListAsync();
        }    
        public async Task<PagedList<PhanLoai>> Get(PhanLoaiSearchModel model)
        {
          
            IEnumerable<PhanLoai> listUnpaged;
            listUnpaged = await _context.PhanLoai.ToListAsync();

            if (!string.IsNullOrWhiteSpace(model.Name))

            {
                listUnpaged = listUnpaged.Where(x => x.TenLoai.ToUpper().Contains(model.Name.ToUpper()));
            }

            return PagedList<PhanLoai>
                .ToPagedList(listUnpaged, model.PageNumber, model.PageSize);
        }

        public async Task<PhanLoai> Get(Guid id)
        {
            return await _context.PhanLoai.FindAsync(id);
        }
        public async Task<PhanLoai> Add(PhanLoai model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return model;
            }
            catch(Exception)
            {

                return null;
            }
           

        }
        public async Task<PhanLoai> Update(PhanLoai model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {

                return null;
            }


        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var item = _context.PhanLoai.Find(id);
                
                _context.PhanLoai.Remove(item);
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
