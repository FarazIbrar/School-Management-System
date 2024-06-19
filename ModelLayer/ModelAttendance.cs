using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ModelAttendance
    {

        public int attendanceID { get; set; }
        public int regID { get; set; }
        public DateTime attendanceDate { get; set; }
        public string? status{ get; set; }
        public string? studentName { get; set; }  // Optional: for displaying student names


    }
}
