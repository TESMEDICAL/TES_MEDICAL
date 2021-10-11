

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
    public class DichVusvc : IDichVu
    {
        private static int pageSize = 6;
         private readonly DataContext _context;

        public DichVusvc(DataContext context)
        {
            _context = context;

        }
      
      


        
        
        public async Task<DichVu> Add(DichVu model)
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

        public async Task <DichVu> Get(Guid id)
        {
            
            var item = await _context.DichVu
                            
                .FirstOrDefaultAsync(i => i.MaDV == id);
               

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public async Task <DichVu>  Edit(DichVu model )
        {
           try
            {
             using (var transaction = _context.Database.BeginTransaction())
                {
                     
              

                var existingDichVu = _context.DichVu.Find(model.MaDV);
                                     existingDichVu.TenDV = model.TenDV;
                                       existingDichVu.DonGia = model.DonGia;
                                   
              



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
                
                    var find = await _context.DichVu.FindAsync(Id);
                   

                    _context.DichVu.Remove(find);
                    await _context.SaveChangesAsync();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }
                
       
        public async Task<IPagedList<DichVu>> SearchByCondition(DichVuSearchModel model)
        {

                IEnumerable<DichVu> listUnpaged;
                listUnpaged = _context.DichVu.OrderBy(x=>x.TenDV) ;
               
                

                                                                                         if(!string.IsNullOrWhiteSpace(model.TenDVSearch)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.TenDV.ToUpper().Contains(model.TenDVSearch.ToUpper()));
                   }
                         
                                  
                                                                        if(!string.IsNullOrWhiteSpace(model.DonGiaSearch.ToString())) 
                                
                    {
                     listUnpaged = listUnpaged.Where(x => x.DonGia==model.DonGiaSearch);
                    }
   
     

                                     
                                           
              
                        
             
               
                var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<DichVu> GetAllFromDatabase()
        {
            List<DichVu> data = new List<DichVu>();
            
                data = _context.DichVu.ToList();
                
            
            return data;
            
        }
    }
}


