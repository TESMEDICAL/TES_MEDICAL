

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
    public class NhanVienYtesvc : INhanVienYte
    {
        private static int pageSize = 6;
         private readonly DataContext _context;

        public NhanVienYtesvc(DataContext context)
        {
            _context = context;

        }
      
      


        
        
        public async Task <NhanVienYte> Add(NhanVienYte model)
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
                
            }     
        }

        public async Task <NhanVienYte> Get(Guid id)
        {
            
            var item = await _context.NhanVienYte
                            
                .FirstOrDefaultAsync(i => i.MaNV == id);
               

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public async Task <NhanVienYte>  Edit(NhanVienYte model )
        {
           try
            {
             using (var transaction = _context.Database.BeginTransaction())
                {
                     
              

                var existingNhanVienYte = _context.NhanVienYte.Find(model.MaNV);
                                     existingNhanVienYte.EmailNV = model.EmailNV;
                                       existingNhanVienYte.MatKhau = model.MatKhau;
                                       existingNhanVienYte.HoTen = model.HoTen;
                                       existingNhanVienYte.SDTNV = model.SDTNV;
                                       existingNhanVienYte.ChucVu = model.ChucVu;
                                       existingNhanVienYte.TrangThai = model.TrangThai;
                                       existingNhanVienYte.Hinh = model.Hinh;
                                       existingNhanVienYte.ChuyenKhoa = model.ChuyenKhoa;
                                   
              



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

       public async Task <bool> Delete(Guid Id)
        {
            try
            {
                
                    var find = await _context.NhanVienYte.FindAsync(Id);
                   

                    _context.NhanVienYte.Remove(find);
                    await _context.SaveChangesAsync();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }
                 public  IEnumerable<ChuyenKhoa> ChuyenKhoaNav()
        {
            return _context.ChuyenKhoa.ToList();
        }

                 
       
        public async Task<IPagedList<NhanVienYte>> SearchByCondition(NhanVienYteSearchModel model)
        {

                IEnumerable<NhanVienYte> listUnpaged;
                listUnpaged = _context.NhanVienYte.OrderBy(x=>x.EmailNV) ;
               
                

                                                                                                                                                             if(!string.IsNullOrWhiteSpace(model.HoTenSearch)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.HoTen.ToUpper().Contains(model.HoTenSearch.ToUpper()));
                   }
                         
                                  
                                                                        if(!string.IsNullOrWhiteSpace(model.SDTNVSearch)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.SDTNV.ToUpper().Contains(model.SDTNVSearch.ToUpper()));
                   }
                         
                                  
                                                                                                                                                                              if(!string.IsNullOrWhiteSpace(model.ChuyenKhoaSearch.ToString())) 
                                
                    {
                     listUnpaged = listUnpaged.Where(x => x.ChuyenKhoa==model.ChuyenKhoaSearch);
                    }
   
     

                                     
                                           
              
                        
             
               
                var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<NhanVienYte> GetAllFromDatabase()
        {
            List<NhanVienYte> data = new List<NhanVienYte>();
            
                data = _context.NhanVienYte.ToList();
                
            
            return data;
            
        }
    }
}


