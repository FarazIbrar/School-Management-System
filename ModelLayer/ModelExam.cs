using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ModelExam
    {
        public int examID { get; set; }
        public int regID { get; set; }
        public string? examType { get; set; }
        public  decimal  totalMarks{ get; set; }
        public decimal obtainedMarks { get; set; }

    }
}
