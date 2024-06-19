using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ModelSalary
    {
        public int salaryID { get; set; }
        public int teacherID { get; set; }
        public decimal totalSalary { get; set; }
        public string? salaryStatus { get; set; }
        public int salaryMonth { get; set; }
        public int salaryYear { get; set; }
        public DateTime payDate { get; set; }

    }
}
