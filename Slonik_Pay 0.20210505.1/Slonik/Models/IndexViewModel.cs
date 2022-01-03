using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Detal> Detals { get; set; }
        public IEnumerable<Coefic> Coefic { get; set; }
        public IEnumerable<Arhiv> Arhiv { get; set; }
        public PageInfo PageInfo { get; set; } //Хранит информацию о текущей странице
    }
}