using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TES_MEDICAL.GUI.Controllers
{
    public class NhanVienYTeController : Controller
    {
        private readonly INhanVienYte _service;
        public NhanVienYTeController(INhanVienYte service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(NhanVienYteSearchModel model)
        {

            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _service.SearchByCondition(model);
            ViewBag.ChuyenKhoa = _service.ChuyenKhoaNav();


            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new NhanVienYteSearchModel());
        }

        [HttpGet]
        public async Task<ActionResult> PageList(NhanVienYteSearchModel model)
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

        public async Task<ActionResult> Add()
        {
            ViewBag.ChuyenKhoa = new SelectList(_service.ChuyenKhoaNav(), "MaCK", "TenCK");

            return PartialView("_partialAdd", new NhanVienYte());

        }

        [HttpPost]
        public async Task<ActionResult> Add(NhanVienYte model)
        {

            model.MaNV = Guid.NewGuid();
            if (await _service.Add(model) != null)
                return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


        }


        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var item = await _service.Get(id);
            if (item == null)
            {
                return NotFound(); ;
            }
            else
            {
                ViewBag.ChuyenKhoa = new SelectList(_service.ChuyenKhoaNav(), "MaCK", "TenCK", item.ChuyenKhoa);


                return PartialView("_partialedit", item);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Detail(Guid id)
        {
            var item = await _service.Get(id);
            if (item == null)
            {
                return NotFound(); ;
            }
            else
            {
                ViewBag.ChuyenKhoa = new SelectList(_service.ChuyenKhoaNav(), "MaCK", "TenCK", item.ChuyenKhoa);


                return PartialView("_partialDetail", item);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(NhanVienYte model)
        {

            if (await _service.Edit(model) != null)
                return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _service.Delete(id))
                return Json(new { status = 1, title = "", text = "Xoá thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Xoá không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }



        public IActionResult ThemNvYTe()
        {
            return PartialView("_AddNhanVienYTe");
        }

        public IActionResult EditNhanVienYTe()
        {
            return PartialView("_EditNhanVienYTe");
        }

        public IActionResult DetailNhanVienYTe()
        {
            return PartialView("_DetailNhanVienYTe");
        }
    }
}
