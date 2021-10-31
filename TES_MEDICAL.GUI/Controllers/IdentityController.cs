using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<NhanVienYte> _userManager;
        private readonly SignInManager<NhanVienYte> _signInManager;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(SignInManager<NhanVienYte> signInManager, ILogger<IdentityController> logger, UserManager<NhanVienYte> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ThemPhieuKham", "TiepNhan");
            
        }

        [HttpPost]
        public async Task<IActionResult> ChangeInfo(NhanVienModel model, [FromForm] IFormFile file)
        {
            try
            {
                string filePath = "";
                

                var user = await _userManager.GetUserAsync(User);
                user.HoTen = model.HoTen;
                user.PhoneNumber = model.SDTNV;
                
                if (file != null)
                {
                    var fileName = Path.GetFileName(DateTime.Now.ToString("ddMMyyyyss") + file.FileName);
                    user.Hinh = fileName;
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                }

                if (file != null)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                }

                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);

                return Json(new { status = 1, title = "", text = "Cập nhật thành công."}, new Newtonsoft.Json.JsonSerializerSettings());
            }

            catch (Exception)
            {

                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            } 
        }


    }
}
