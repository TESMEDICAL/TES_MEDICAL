using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    public class ReportController : Controller
    {
        private IHostingEnvironment Environment;
        private readonly IReport _service;
        
        public ReportController(IHostingEnvironment _environment, IReport service)
        {
            Environment = _environment;
            _service = service;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReportBenhNhan()
        {
            return View();
        }

        //Xem và tải hoá đơn dịch vụ
        public async Task<IActionResult> ViewHoaDon()
        {

            //string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "HoaDon/"));
            //List<FileModel> files = new List<FileModel>();

            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new FileModel { FileName = Path.GetFileName(filePath) });

            //}

            return View(await _service.GetAllHoaDon());

        }

        [HttpGet]
        public async Task<IActionResult> Detail(string MaHD)
        {
            if (await _service.Get(MaHD) == null)
            {
                return NotFound(); ;
            }
            else
            {


                return PartialView("_partialDetail", await _service.Get(MaHD));
            }
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "HoaDon/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }


        //Xem và tải hoá đơn thuốc
        public IActionResult ViewHoaDonThuoc()
        {

            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "HoaDon/HoaDonThuoc/"));
            List<FileModel> files = new List<FileModel>();

            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }

            return View(files);
        }

        public FileResult DownloadFile1(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "HoaDon/HoaDonThuoc/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }




    }
}
