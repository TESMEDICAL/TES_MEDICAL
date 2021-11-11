

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using X.PagedList;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using Microsoft.Data.SqlClient;
using TES_MEDICAL.ENTITIES.Models.ViewModel;

namespace TES_MEDICAL.GUI.Services
{
    public class Benhsvc : IBenh
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public Benhsvc(DataContext context)
        {
            _context = context;

        }


        public async Task<Response<Benh>> Add(Benh model, List<CTrieuChungModel> TrieuChungs)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(model).State = EntityState.Added;
                    await _context.SaveChangesAsync();
                    Guid id = model.MaBenh;
                    if (TrieuChungs.Count() > 0)
                    {
                        foreach (var item in TrieuChungs)
                        {
                            item.MaBenh = id;
                            List<SqlParameter> parms = new List<SqlParameter>
                            {

                                new SqlParameter { ParameterName = "@Mabenh", Value= item.MaBenh },
                                new SqlParameter { ParameterName = "@TenTrieuChung", Value= item.TenTrieuChung },
                                new SqlParameter { ParameterName = "@ChiTietTrieuChung", Value= item.ChiTietTrieuChung }
                            };
                            var result = _context.Database.ExecuteSqlRaw("EXEC dbo.AddCTrieuChung @Mabenh,@TenTrieuChung,@ChiTietTrieuChung", parms.ToArray());

                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new Response<Benh> { errorCode = 0, Obj = model };

                }

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return new Response<Benh> { errorCode = -1 };
                }

                return new Response<Benh> { errorCode = -2 };
            }
        }

        public async Task<Benh> Get(Guid id)
        {

            var item = await _context.Benh
                            .Include(p => p.CTTrieuChung).ThenInclude(x=>x.MaTrieuChungNavigation)
                            .FirstOrDefaultAsync(i => i.MaBenh == id);

            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<Response<Benh>> Edit(Benh model,List<CTrieuChungModel> trieuchungs)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var listCTTrieuChung = _context.CTTrieuChung.Where(p => p.MaBenh == model.MaBenh);
                    foreach (var existingCTTrieuChung in listCTTrieuChung)
                    {
                        if (!trieuchungs.Any(c => c.MaBenh == existingCTTrieuChung.MaBenh))

                            _context.Entry(existingCTTrieuChung).State = EntityState.Deleted;
                    }

                    foreach (var item in trieuchungs)
                    {

                        if (item.MaBenh == Guid.Empty)
                        {

                            item.MaBenh = model.MaBenh;

                            List<SqlParameter> parms = new List<SqlParameter>
                            {

                                new SqlParameter { ParameterName = "@Mabenh", Value= item.MaBenh },
                                new SqlParameter { ParameterName = "@TenTrieuChung", Value= item.TenTrieuChung },
                                 new SqlParameter { ParameterName = "@ChiTietTrieuChung", Value= item.ChiTietTrieuChung }
                            };
                            var result = _context.Database.ExecuteSqlRaw("EXEC dbo.AddCTrieuChung @Mabenh,@TenTrieuChung,@ChiTietTrieuChung", parms.ToArray());


                        }


                      
                    }

                    var existingBenh = await _context.Benh.FindAsync(model.MaBenh);
                    existingBenh.TenBenh = model.TenBenh;
                    existingBenh.ThongTin = model.ThongTin;
                    existingBenh.MaCK = model.MaCK;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new Response<Benh> { errorCode = 0, Obj = model };
                }


            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return new Response<Benh> { errorCode = -1 };
                }

                return new Response<Benh> { errorCode = -2 };
            }




        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {

                var find = await _context.Benh.FindAsync(Id);


                _context.Benh.Remove(find);
                await _context.SaveChangesAsync();

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }


        }
        public async Task<IEnumerable<ChuyenKhoa>> ChuyenKhoaNav()
        {
            return await _context.ChuyenKhoa.ToListAsync();
        }



        public async Task<IPagedList<Benh>> SearchByCondition(BenhSearchModel model)
        {

            IEnumerable<Benh> listUnpaged;
            listUnpaged = _context.Benh.Include(x=>x.MaCKNavigation).OrderBy(x => x.TenBenh);

            if (!string.IsNullOrWhiteSpace(model.TenBenhSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.TenBenh.ToUpper().Contains(model.TenBenhSearch.ToUpper()));
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



        protected IEnumerable<Benh> GetAllFromDatabase()
        {
            List<Benh> data = new List<Benh>();

            data = _context.Benh.ToList();


            return data;

        }
    }
}


