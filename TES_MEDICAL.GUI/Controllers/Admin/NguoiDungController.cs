
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using TES_MEDICAL.GUI.Controllers.Admin;
using TES_MEDICAL.GUI.Constant;

namespace TES_MEDICAL.GUI.Controllers
{
   
    public class NguoiDungController : BaseController
    {
        private readonly INguoiDung _service;
        public NguoiDungController(INguoiDung service)
        {
            _service = service;
        }

        public async Task <ActionResult> Index(NguoiDungSearchModel model)
        {
           
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _service.SearchByCondition(model);
                          
            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new NguoiDungSearchModel());
        }
       
      
        [HttpGet]
       
        public async Task <ActionResult> PageList(NguoiDungSearchModel model)
        {
            
        var listmodel = await _service.SearchByCondition(model);
            if (listmodel.Count() > 0)
            {

                if (!model.Page.HasValue) model.Page = 1;



                
                ViewBag.Names = listmodel;
                ViewBag.Data = model;

                return PartialView("_NameListPartial", listmodel);
            }
            else
            {

                return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }

          
        }
                
        
        public IActionResult Add()
        {
                       
            return PartialView("_partialAdd",new NguoiDung() );

        }

        [HttpPost]
        public async Task <ActionResult> Add([Bind("Email,MatKhau,ConfirmPassword,HoTen,SDT,HinhAnh,ChucVu,TrangThai")] NguoiDung model, [FromForm] IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string filePath = "";
                //IFormFile file = HttpContext.Request.Form.Files[0];
                var filePathDefault = "final.png";

                if (file == null)
                {
                    model.HinhAnh = filePathDefault;
                }
                else
                {
                    //file = HttpContext.Request.Form.Files[0];
                    model.HinhAnh = DateTime.Now.ToString("ddMMyyyyss") + file.FileName;

                    var fileName = Path.GetFileName(DateTime.Now.ToString("ddMMyyyyss") + file.FileName);
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\NguoiDung", fileName);
                }

                model.MaNguoiDung = Guid.NewGuid();

                var result = await _service.Add(model);
                if (result.errorCode == -1)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                    return PartialView("_partialAdd", model);
                }

                if (result.errorCode == 0)
                {
                    if (file != null)
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                    }

                    return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
                else
                {
                    return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
            }
            return PartialView("_partialAdd", model);

        } 
        [HttpGet]
       
        public async Task <ActionResult> Edit(Guid id)
        {
            if (await _service.Get(id) == null)
            {
                return NotFound();;
            }
            else
            {
                     
              return PartialView("_partialedit",await _service.Get(id));
            }
               
        }
          [HttpGet]
        public async Task <ActionResult> Detail(Guid id)
        {
            if (await _service.Get(id) == null)
            {
                return NotFound(); ;
            }
            else
            {
                      
             
                return PartialView("_partialDetail",await _service.Get(id));
            }
        }

       
        [HttpPost]
        public async Task<ActionResult> Edit(NguoiDung model)
        {
            var user = await _service.Get(Guid.Parse(HttpContext.Session.GetString(SessionKey.Nguoidung.MaNguoiDung)));
            user.ChucVu = model.ChucVu;
            user.TrangThai = model.TrangThai;
            var result = await _service.Edit(user);
            if (result.errorCode == 0)
            {
                return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());

            }
            
        }
            
        



        [HttpPost]
        public async Task <ActionResult> Delete(Guid id)
        {
            var user = await _service.Get(id);
            user.TrangThai = false;
            if (await _service.Edit(user)!=null) 
                return Json(new { status = 1, title = "", text = "Vô hiệu  thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Vô hiệu không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<ActionResult> Restore(Guid id)
        {
            var user = await _service.Get(id);
            user.TrangThai = true;
            if (await _service.Edit(user) != null)
                return Json(new { status = 1, title = "", text = "Khôi phục  thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Khôi phục không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}


