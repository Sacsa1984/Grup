using Slonik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.ViveModel
{
    public class AddDetalChertViewModel
    {
        public HttpPostedFileBase File { get; set; }
         public List<int> IntList_L { get; set; }
        public List<int> IntList_T { get; set; }
        public List<int> IntList_D { get; set; }
        public List<int> IntList_TocHnost { get; set; }
        public Detal detalPik { get; set; }
    }
}