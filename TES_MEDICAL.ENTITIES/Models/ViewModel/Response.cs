using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models
{
    public class Response<T> where T:class
    {
        public int errorCode { get; set; }
        public T Obj { get; set; }
    }
}
