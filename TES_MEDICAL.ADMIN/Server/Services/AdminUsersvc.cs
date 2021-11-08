using TES_MEDICAL.ADMIN.Server.Models;
using TES_MEDICAL.ADMIN.Server.Paging;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Server.Services
{
    public interface IAdminUser
    {
        Task<PagedList<AdminUser>> Get(AdminUserSearchModel searchModel);
        Task<AdminUser> Get(Guid id);
        Task<AdminUser> Add(AdminUser model);
        Task<AdminUser> Update(AdminUser model);
        Task<bool> Patch(AdminUser model);

    }
    public class AdminUsersvc : IAdminUser
    {
        private readonly DataContext _context;
        public AdminUsersvc(DataContext context)
        {
            _context = context;
        }
        public async Task<PagedList<AdminUser>> Get(AdminUserSearchModel model)
        {
            IEnumerable<AdminUser> listUnpaged;
            listUnpaged = await _context.AdminUser.ToListAsync();

            if (!string.IsNullOrWhiteSpace(model.Name))

            {
                listUnpaged = listUnpaged.Where(x => x.Name.ToUpper().Contains(model.Name.ToUpper()));
            }
            if (model.TrangThai == false) listUnpaged = listUnpaged.Where(x => x.TrangThai == true);





            return PagedList<AdminUser>
                .ToPagedList(listUnpaged, model.PageNumber, model.PageSize);
        }

        public async Task<AdminUser> Get(Guid id)
        {
            return await _context.AdminUser.FindAsync(id);
        }
        public async Task<AdminUser> Add(AdminUser model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {

                return null;
            }


        }
        public async Task<AdminUser> Update(AdminUser model)
        {
            try
            {
                var item = _context.AdminUser.Find(model.Id);
                item.Name = model.Name;
                item.UserName = model.UserName;
                item.Quyen = model.Quyen;
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
        public async Task<bool> Patch(AdminUser model)
        {
            try
            {
                var item = _context.AdminUser.Find(model.Id);
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
