using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAppMvc.Models
{
    public class TotalMark
    {
        public int StudentId { get; set; }

        //public int Total
        //{
        //    get { return Math + English + Literature; }
        //    set { }
        //}

        //public float Average
        //{
        //    get { return Total / 3; }
        //    set { }
        //}
        public int Total { get; set; }
        public int Average { get; set; }
    }
}

