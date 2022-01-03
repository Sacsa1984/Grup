using Slonik.BaseContex;
using Slonik.Interfaces;
using Slonik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Repository
{
    public class RepositorCoefic:ISlonik<Coefic>, IDisposable
    {
        ContexSlonik contexSlonik;
        List<Coefic> Coef;
        public RepositorCoefic()
        {
            contexSlonik = new ContexSlonik();
            Coef = contexSlonik.coefics.ToList();

        }
        public void Add(Coefic coefic)//добавить деталь
        {
            contexSlonik.coefics.Add(coefic);
            contexSlonik.SaveChanges();
            Coef = contexSlonik.coefics.ToList();
        }

        public void Delete(int id)
        {

            contexSlonik.coefics.Remove(contexSlonik.coefics.Find(id));
            contexSlonik.SaveChanges();
            Coef = contexSlonik.coefics.ToList();

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EFUnitOfWork()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public Coefic EditInd(int ind)
        {
            throw new NotImplementedException();
        }

        public Coefic EditNazvanie(string Nazvanie)
        {
            throw new NotImplementedException();
        }

        public Coefic EditOrder(int NumOrder)
        {
            throw new NotImplementedException();
        }

        public Detal Find(FindModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coefic> GetList()//возврат данных коофициэнта
        {

            IEnumerable<Coefic> coef = contexSlonik.coefics.ToList();

            return coef;

        }

        

        public void Update(Coefic entity)
        {
            throw new NotImplementedException();
        }

        public Coefic Кosshot(int id)
        {
            throw new NotImplementedException();
        }
    }
}