
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

    public class BenhController : Controller
    {
        private readonly IBenh _service;
        public BenhController(IBenh service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(BenhSearchModel model)
        {

            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _service.SearchByCondition(model);
            ViewBag.MaCK = _service.ChuyenKhoaNav();


            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new BenhSearchModel());
        }


        [HttpGet]

        public async Task<ActionResult> PageList(BenhSearchModel model)
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
        public ActionResult addCTTrieuChung()
        {

            return PartialView("_CTTrieuChungView", new CTTrieuChung());
        }


        public async Task<ActionResult> Add()
        {
            ViewBag.MaCK = new SelectList(await _service.ChuyenKhoaNav(), "MaCK", "TenCK");

            return PartialView("_partialAdd", new Benh());

        }
        [HttpPost]


        public async Task<ActionResult> Add(Benh model)
        {

            model.MaBenh = Guid.NewGuid();
            if (await _service.Add(model) != null)
                return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


        }
        [HttpGet]

        public async Task<ActionResult> Edit(Guid id)
        {
            if (await _service.Get(id) == null)
            {
                return NotFound(); ;
            }
            else
            {
                ViewBag.MaCK = new SelectList(await _service.ChuyenKhoaNav(), "MaCK", "TenCK", (await _service.Get(id)).MaCK);


                return PartialView("_partialedit", await _service.Get(id));
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
                ViewBag.MaCK = new SelectList(await _service.ChuyenKhoaNav(), "MaCK", "TenCK", (await _service.Get(id)).MaCK);


                return PartialView("_partialDetail", await _service.Get(id));
            }
        }


        [HttpPost]

        public async Task<ActionResult> Edit(Benh model)
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
    }
}


