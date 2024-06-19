using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ModelLayer
{
    public class ModelStudent
    {
          
        public int regID { get; set; }
        public string? name{ get; set; }
        public string? grade { get; set; }
        public string? section { get; set; }
        public string? gender { get; set; }
        public DateTime DOB { get; set; }
        public string? cnic { get; set; }
        public string? admissionNo { get; set; }
        public DateTime admissionDate { get; set; }
        public string? address { get; set; }
        public int parentID { get; set; }
        public string? studentEmail { get; set; }
        public string? studentPassword { get; set; }

    }
}
