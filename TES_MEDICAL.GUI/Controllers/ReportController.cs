using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    public class ReportController : Controller
    {
        private IHostingEnvironment Environment;
        public ReportController(IHostingEnvironment _environment)
        {
            Environment = _environment;
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
        public IActionResult ViewHoaDon()
        {

            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "HoaDon/"));
            List<FileModel> files = new List<FileModel>();

            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }

            return View(files);

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
