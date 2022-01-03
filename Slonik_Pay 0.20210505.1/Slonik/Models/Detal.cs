using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Models
{
    public class Detal
    {
        public int Id { get; set; }
        public int NumOrder { get; set; }
        public double StoimostDet { get; set; }
        public string Nazvanie { get; set; }
        public double D { get; set; }
        public double dO { get; set; }
        public double WeithDetail { get; set; }
        public string Materia { get; set; }
        public int Hurd { get; set; }
        public int CoeficId { get; set; }
        public string Picture { get; set; }
        public double LDetal { get; set; }
        public string TypeObr { get; set; }
        public double LProt { get; set; }
        public double Glubina { get; set; }
        public int Tochnost { get; set; }
        public double StoimostRashot { get; set; }


        public virtual Coefic Coefic { get; set; }
        public virtual Users Users { get; set; }
    }
}