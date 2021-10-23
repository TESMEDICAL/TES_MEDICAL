
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

    public class TinTucController : Controller
    {
        private readonly ITinTuc _service;
        private readonly ITheLoai _theLoaiRep;

        public TinTucController(ITinTuc service, ITheLoai theLoaiRep)
        {
            _service = service;
            _theLoaiRep = theLoaiRep;
        }

        public async Task<ActionResult> Index(TinTucSearchModel model)
        {

            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _service.SearchByCondition(model);
            ViewBag.MaNguoiViet = _service.NguoiDungNav();

            ViewBag.MaTL = await _theLoaiRep.GetAll();


            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new TinTucSearchModel());
        }


        [HttpGet]

        public async Task<ActionResult> PageList(TinTucSearchModel model)
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
            ViewBag.MaNguoiViet = new SelectList(_service.NguoiDungNav(), "MaNguoiDung", "Email");

            return PartialView("_partialAdd", new TinTuc());

        }
        [HttpPost]
        


        public async Task<ActionResult> ThemTinTuc(TinTuc model)
        {
            NguoiDung nguoiDung = new NguoiDung();
            nguoiDung.MaNguoiDung = Guid.Parse("A5FB55E8-25AF-434D-90E3-25E74B222882");

            model.MaBaiViet = Guid.NewGuid();
            model.MaNguoiViet = nguoiDung.MaNguoiDung;
            if (await _service.Add(model) != null)
                return /*Json(new { status = 1, title = "", text = "Thêm thành công.", redirectUrL = Url.Action("Index", "TinTuc"), obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());*/
                    RedirectToAction("index", "Tintuc");
            else
                return View(model);
            //    return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


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
                ViewBag.MaNguoiViet = new SelectList(_service.NguoiDungNav(), "MaNguoiDung", "Email", (await _service.Get(id)).MaNguoiViet);


                return View(item);
            }

        }
        [HttpGet]
        public async Task<ActionResult> Detail(Guid id)
        {
            if (await _service.Get(id) == null)
            {
                return NotFound(); ;
            }
            else
            {
                ViewBag.MaNguoiViet = new SelectList(_service.NguoiDungNav(), "MaNguoiDung", "Email", (await _service.Get(id)).MaNguoiViet);


                return PartialView("_partialDetail", await _service.Get(id));
            }
        }


        [HttpPost]

        public async Task<ActionResult> Edit(TinTuc model)
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


        public async Task<ActionResult> ThemTinTuc()
        {
            ViewBag.MaTL = new SelectList(await _theLoaiRep.GetAll(),"MaTL","TenTL");
            return View();
        }
    }
}


