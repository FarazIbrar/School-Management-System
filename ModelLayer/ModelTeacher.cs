using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ModelTeacher
    {
        public int teacherID { get; set; }
        public string? teacherName { get; set; }
        public string? teacherFatherName { get; set; }
        public DateTime joiningDate { get; set; }
        public string? teacherGender { get; set; }
        public DateTime teacherDOB { get; set; }
        public string? teacherCnic{ get; set; }
        public string? teacherPhoneNo { get; set; }
        public string? teacherAddress { get; set; }
        public string? teacherEducation { get; set; }
        public string? teacherPastExperience { get; set; }
        public string? teacherEmail { get; set; }
        public string? teacherPassword { get; set; }
    }
}
