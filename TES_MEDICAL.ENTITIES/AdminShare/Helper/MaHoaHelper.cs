using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Shared.Helper
{
    public static  class MaHoaHelper
    {
        public static string Mahoa(string source)
        {
            string hash = "";
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
            return hash;
        }
    }
}
