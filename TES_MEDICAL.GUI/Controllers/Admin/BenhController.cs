
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Controllers.Admin;

namespace TES_MEDICAL.GUI.Controllers
{

    public class BenhController : BaseController
    {
        private readonly IBenh _service;
        public BenhController(IBenh service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(BenhSearchModel model)
        {

            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _service.SearchByCondition(model);
            ViewBag.MaCK = await _service.ChuyenKhoaNav();


            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new BenhSearchModel());
        }


        [HttpGet]

        public async Task<IActionResult> PageList(BenhSearchModel model)
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
        public IActionResult addCTTrieuChung()
        {

            return PartialView("_CTTrieuChungView", new CTrieuChungModel());
        }


        public async Task<IActionResult> Add()
        {
            ViewBag.MaCK = new SelectList(await _service.ChuyenKhoaNav(), "MaCK", "TenCK");

            return PartialView("_partialAdd", new Benh());

        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Benh model,CTrieuChungModel[] Trieuchungs)
        {

            model.MaBenh = Guid.NewGuid();
            if (await _service.Add(model,Trieuchungs.ToList()) != null)
                return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


        }
        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _service.Get(id);
            if (item == null)
            {
                return NotFound(); ;
            }
            else
            {
                var listTC = new List<CTrieuChungModel>();
                ViewBag.MaCK = new SelectList(await _service.ChuyenKhoaNav(), "MaCK", "TenCK", (await _service.Get(id)).MaCK);

                foreach(var trieuchung in item.CTTrieuChung)
                {
                    listTC.Add(new CTrieuChungModel { MaBenh = item.MaBenh, MaTrieuChung = trieuchung.MaTrieuChung, TenTrieuChung = trieuchung.MaTrieuChungNavigation.TenTrieuChung, ChiTietTrieuChung = trieuchung.ChiTietTrieuChung });
                }
                ViewBag.ListTC = listTC;
                return PartialView("_partialedit", await _service.Get(id));
            }

        }
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Benh model, CTrieuChungModel[] Trieuchungs)
        {

            if (await _service.Edit(model,Trieuchungs.ToList()) != null)
                return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _service.Delete(id))
                return Json(new { status = 1, title = "", text = "Xoá thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Xoá không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}


