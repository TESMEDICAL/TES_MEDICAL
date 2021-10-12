
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

    public class ChuyenKhoaController : Controller
    {
        private readonly IChuyenKhoa _service;
        private readonly IValidate _valiservice;
        public ChuyenKhoaController(IChuyenKhoa service, IValidate valiservice)
        {
            _service = service;
            _valiservice = valiservice;
        }

        public async Task <ActionResult> Index(ChuyenKhoaSearchModel model)
        {

            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _service.SearchByCondition(model);

            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new ChuyenKhoaSearchModel());
        }


        [HttpGet]

        public async Task<ActionResult> PageList(ChuyenKhoaSearchModel model)
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


        public ActionResult Add()
        {

            return PartialView("_partialAdd", new ChuyenKhoa());

        }
        [HttpPost]


        public async Task<ActionResult> Add(ChuyenKhoa model)
        {

            model.MaCK = Guid.NewGuid();
            if (await _service.Add(model) != null)
                return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


        }
        public async Task<IActionResult> ValidateChuyenKhoa(string TenCK)
        {
            if (await _valiservice.ExistsChuyenKhoa(TenCK) )
            {
                return Json(data: "Tên chuyên khoa đã tồn tại");
            }
            return Json(data: true);
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

                return PartialView("_partialedit",await _service.Get(id));
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
                return PartialView("_partialDetail", _service.Get(id));
            }
        }


        [HttpPost]

        public async Task<ActionResult> Edit(ChuyenKhoa model)
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


