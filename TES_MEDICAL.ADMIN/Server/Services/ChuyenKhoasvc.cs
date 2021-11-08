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
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.ADMIN.Server.Helpers;
using Microsoft.Data.SqlClient;

namespace TES_MEDICAL.ADMIN.Server.Services
{
    public interface IChuyenKhoa
    {
        Task<IEnumerable<ChuyenKhoa>> Get();
        Task<PagedList<ChuyenKhoa>> Get(ChuyenKhoaApiSearchModel model);
        Task<ChuyenKhoa> Get(Guid id);
        Task<ChuyenKhoa> Add(ChuyenKhoa model);
        Task<ChuyenKhoa> Update(ChuyenKhoa model);
       

    }
    public class ChuyenKhoasvc:IChuyenKhoa
    {
        private readonly DataContext _context;
        public ChuyenKhoasvc(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChuyenKhoa>> Get()
        {
            return await _context.ChuyenKhoa.ToListAsync();
        }    
        public async Task <PagedList<ChuyenKhoa>> Get(ChuyenKhoaApiSearchModel model)
        {
          
            IEnumerable<ChuyenKhoa> listUnpaged;
            listUnpaged = await _context.ChuyenKhoa.FromSqlRaw("EXEC dbo.SearchChuyenkhoa @KeyWord",new SqlParameter("KeyWord",string.IsNullOrWhiteSpace(model.Name)?DBNull.Value:model.Name)).ToListAsync();

          

            return PagedList<ChuyenKhoa>
                .ToPagedList(listUnpaged, model.PageNumber, model.PageSize);
        }

        public async Task<ChuyenKhoa> Get(Guid id)
        {
            return await _context.ChuyenKhoa.FindAsync(id);
        }
        public async Task<ChuyenKhoa> Add(ChuyenKhoa model)
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
        public async Task<ChuyenKhoa> Update(ChuyenKhoa model)
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
                var item = _context.ChuyenKhoa.Find(id);
                
                _context.ChuyenKhoa.Remove(item);
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
