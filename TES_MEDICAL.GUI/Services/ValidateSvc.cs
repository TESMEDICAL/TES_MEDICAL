﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Services
{
    public class ValidateSvc : IValidate
    {
        private readonly DataContext _context;
        public ValidateSvc(DataContext context)
        {
            _context = context;
        }
        public bool CheckNgayKham(DateTime? ngay)
        {
            return ngay > DateTime.Now;
        }
        public bool ExistsChuyenKhoa(string ten)
        {
            return  _context.ChuyenKhoa.Any(x => x.TenCK == ten);
        }
    }
}
