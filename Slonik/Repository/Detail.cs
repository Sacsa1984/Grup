using Slonik.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Slonik.Models;

namespace Slonik.Repository
{
    public class Detail : ISlonik<T>
    {
        public void Add(T detal)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(string Nazvanie)
        {
            throw new NotImplementedException();
        }

        public void Edit(int NumOrder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList(int NumOrder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList(string Nazvanie)
        {
            throw new NotImplementedException();
        }
    }
}