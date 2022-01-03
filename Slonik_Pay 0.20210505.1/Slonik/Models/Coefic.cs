using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Models
{
    public class Coefic
    {
        public int Id { get; set; }
        
        public double coeficientStoim { get; set; }
        public int DopCoeficient { get; set; }
        public double CoeficStanka { get; set; }
        public double CoficTochnost { get; set; }
        public double stinostMM_proto { get; set; }//стоимость обработки одного мм детали
        public int GlubMaxStan { get; set; }//максимальная глубина резанья станка
       public string Stanok { get; set; }
        public virtual ICollection<Detal> Detals { get; set; }
        public Coefic()
        {
            Detals = new List<Detal>();
          
        }
    }
}