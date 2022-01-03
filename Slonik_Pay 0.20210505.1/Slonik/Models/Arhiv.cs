using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Models
{
    public class Arhiv
    {
        public int Id { get; set; }
        public int NumOrder { get; set; }
        public string Nazvanie { get; set; }
        public double StoimostDet { get; set; }
       
        public double WeithDetail { get; set; }
        public string Material { get; set; }
       
        public string TypeObr { get; set; }
        public string Stanok { get; set; }
        public string Picture { get; set; }
       
        public double StoimostRashot { get; set; }


        public virtual Users Users { get; set; }
    }
}