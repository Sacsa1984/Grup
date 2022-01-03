using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Key { get; set; } // Вмещает в себе Login и Password в зашифрованом виде
        public string Name { get; set; }
        public string Dolgnost { get; set; }
        public string Role { get; set; }
        public bool IsConnected { get; set; }
        public virtual ICollection<Detal> Detals { get; set; }
        public Users()
        {
            Detals = new List<Detal>();
        }
    }
}