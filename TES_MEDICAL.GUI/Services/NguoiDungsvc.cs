

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
    public class NguoiDungsvc : INguoiDung
    {
        private static int pageSize = 6;
         private readonly DataContext _context;

        public NguoiDungsvc(DataContext context)
        {
            _context = context;

        }
      
      


        
        
        public async Task<Response<NguoiDung>> Add(NguoiDung model)
        {
            try
            {
             using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(model).State = EntityState.Added;
                    await _context.SaveChangesAsync();
                            


                 await _context.SaveChangesAsync();
                 await transaction.CommitAsync();
                    return new Response<NguoiDung> { errorCode = 0, Obj = model };

                }
           

                
                   
                   
                   
              

            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return new Response<NguoiDung> { errorCode = -1 };
                }

                return new Response<NguoiDung> { errorCode = -2 };
            }     
        }

        public async Task <NguoiDung> Get(Guid id)
        {
            
            var item = await _context.NguoiDung
                            
                .FirstOrDefaultAsync(i => i.MaNguoiDung == id);
               

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public async Task <Response<NguoiDung>>  Edit(NguoiDung model )
        {
           try
            {
                 using (var transaction = _context.Database.BeginTransaction())
                    {

                    var existingNguoiDung = await _context.NguoiDung.FindAsync(model.MaNguoiDung);
                                         existingNguoiDung.Email = model.Email;
                    //existingNguoiDung.MatKhau = model.MatKhau;
                    existingNguoiDung.HoTen = model.HoTen;
                                           existingNguoiDung.SDT = model.SDT;
                                           existingNguoiDung.HinhAnh = model.HinhAnh;
                                           existingNguoiDung.ChucVu = model.ChucVu;
                                           existingNguoiDung.TrangThai = model.TrangThai;
                                   

                     await _context.SaveChangesAsync();
                     await transaction.CommitAsync();
                    return new Response<NguoiDung> { errorCode = 0, Obj = model };
                }
                
                
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return new Response<NguoiDung> { errorCode = -1 };
                }

                return new Response<NguoiDung> { errorCode = -2 };
            }

               
                    
                 
        }

       public async Task <bool> Delete(Guid Id)
        {
            try
            {
                
                    var find = await _context.NguoiDung.FindAsync(Id);
                   

                    _context.NguoiDung.Remove(find);
                    await _context.SaveChangesAsync();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }
                
       
        public async Task<IPagedList<NguoiDung>> SearchByCondition(NguoiDungSearchModel model)
        {

                IEnumerable<NguoiDung> listUnpaged;
                listUnpaged = _context.NguoiDung.OrderBy(x=>x.Email) ;
               
                

                                                                                                                                                             if(!string.IsNullOrWhiteSpace(model.HoTenSearch)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.HoTen.ToUpper().Contains(model.HoTenSearch.ToUpper()));
                   }
                         
                                  
                                                                        if(!string.IsNullOrWhiteSpace(model.SDTSearch)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.SDT.ToUpper().Contains(model.SDTSearch.ToUpper()));
                   }
                         
                                  
                                                                                                                                            if(!string.IsNullOrWhiteSpace(model.TrangThaiSearch.ToString())) 
                                
                    {
                     listUnpaged = listUnpaged.Where(x => x.TrangThai==model.TrangThaiSearch);
                    }
   
     

                                     
                                           
              
                        
             
               
                var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<NguoiDung> GetAllFromDatabase()
        {
            List<NguoiDung> data = new List<NguoiDung>();
            
                data = _context.NguoiDung.ToList();
                
            
            return data;
            
        }
    }
}


