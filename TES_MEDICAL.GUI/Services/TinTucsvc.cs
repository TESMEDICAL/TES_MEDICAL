

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
    public class TinTucsvc : ITinTuc
    {
        private static int pageSize = 6;
         private readonly DataContext _context;

        public TinTucsvc(DataContext context)
        {
            _context = context;

        }
      
      


        
        
        public async Task <TinTuc> Add(TinTuc model)
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

        public async Task <TinTuc> Get(Guid id)
        {
            
            var item = await _context.TinTuc.Include(x =>x.MaTLNavigation)         
                .FirstOrDefaultAsync(i => i.MaBaiViet == id);
               

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public async Task <TinTuc>  Edit(TinTuc model )
        {
           try
            {
             using (var transaction = _context.Database.BeginTransaction())
                {
                     
              

                var existingTinTuc = _context.TinTuc.Find(model.MaBaiViet);
                                     existingTinTuc.TieuDe = model.TieuDe;
                                       existingTinTuc.NoiDung = model.NoiDung;
                                       existingTinTuc.TrangThai = model.TrangThai;
                                       existingTinTuc.MaNguoiViet = model.MaNguoiViet;
                                       existingTinTuc.MaTL = model.MaTL;
                                   
              



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
                
                    var find = await _context.TinTuc.FindAsync(Id);
                   

                    _context.TinTuc.Remove(find);
                    await _context.SaveChangesAsync();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }
                 public  IEnumerable<NguoiDung> NguoiDungNav()
        {
            return _context.NguoiDung.ToList();
        }

                 
       
        public async Task<IPagedList<TinTuc>> SearchByCondition(TinTucSearchModel model)
        {

                IEnumerable<TinTuc> listUnpaged;
                listUnpaged = _context.TinTuc.OrderBy(x=>x.TieuDe) ;
               
                

                                                                                         if(!string.IsNullOrWhiteSpace(model.TieuDeSearch)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.TieuDe.ToUpper().Contains(model.TieuDeSearch.ToUpper()));
                   }
                         
                                  
                                                                                                          if(!string.IsNullOrWhiteSpace(model.TrangThaiSearch.ToString())) 
                                
                    {
                     listUnpaged = listUnpaged.Where(x => x.TrangThai==model.TrangThaiSearch);
                    }
   
     

                                     
                                                                                                          if(!string.IsNullOrWhiteSpace(model.MaTLSearch.ToString())) 
                                
                    {
                     listUnpaged = listUnpaged.Where(x => x.MaTL==model.MaTLSearch);
                    }
   
     

                                     
                                           
              
                        
             
               
                var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<TinTuc> GetAllFromDatabase()
        {
            List<TinTuc> data = new List<TinTuc>();
            
                data = _context.TinTuc.ToList();
                
            
            return data;
            
        }
    }
}


