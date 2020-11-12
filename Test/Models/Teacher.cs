using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Teacher
    {
        public int teacherid;
        public string teacherfname;
        public string teacherlname;
        public string employeenumber;
        public DateTime hireDate;//try datetime from footer in layout. It works! Woo for learning by trial and error
        public decimal salary;
    }
}