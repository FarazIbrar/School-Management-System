using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ModelFee
    {
        public int feeID { get; set; }
        public int regID { get; set; }
        public decimal totalFee { get; set; }
        public decimal submitedFee { get; set; }
        public string? status { get; set; }
        public int feeMonth { get; set; }
        public int feeYear { get; set; }
        public DateTime submissionDate { get; set; }
    }

}
