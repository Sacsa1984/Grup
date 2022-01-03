using Slonik.BaseContex;
using Slonik.Interfaces;
using Slonik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Repository
{
    public class RepozUser : ISlonik<Users>, IDisposable
    {
        ContexSlonik contexSlonik1;

        public RepozUser()
        {
            contexSlonik1 = new ContexSlonik();
        }
        public void Add(Users detal)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Users EditInd(int ind)
        {
            throw new NotImplementedException();
        }

        public Users EditNazvanie(string Nazvanie)
        {
            throw new NotImplementedException();
        }

        public Users EditOrder(int NumOrder)
        {
            throw new NotImplementedException();
        }

        public Detal Find(FindModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetList()
        {
            IEnumerable<Users> users1 = contexSlonik1.users.ToList();
              return users1;
        }

        public void Update(Users entity)
        {
            throw new NotImplementedException();
        }

        public void Кosshot(int id)
        {
            throw new NotImplementedException();
        }

        Users ISlonik<Users>.Кosshot(int id)
        {
            throw new NotImplementedException();
        }
    }
}