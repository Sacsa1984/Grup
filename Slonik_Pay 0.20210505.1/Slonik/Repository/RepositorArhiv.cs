using Slonik.BaseContex;
using Slonik.Interfaces;
using Slonik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Repository
{
    public class RepositorArhiv: ISlonik<Arhiv>, IDisposable
    {
        ContexSlonik contexSlonik;
        List<Arhiv> Arhi;
        public RepositorArhiv()
        {
            contexSlonik = new ContexSlonik();
            Arhi = contexSlonik.arhivs.ToList();

        }
        public void Add(Arhiv arhiv)//добавить деталь
        {
            contexSlonik.arhivs.Add(arhiv);
            contexSlonik.SaveChanges();
        }
        public void Delete(int id)
        {

            contexSlonik.detals.Remove(contexSlonik.detals.Find(id));
            contexSlonik.SaveChanges();

        }
        public Arhiv EditInd(int ind)
        {
            return contexSlonik.arhivs.FirstOrDefault(item => item.Id == ind);
        }
        public Arhiv EditOrder(int NumOrder)
        {
            return contexSlonik.arhivs.FirstOrDefault(item => item.NumOrder == NumOrder);
        }
        public IEnumerable<Arhiv> GetList()//возврат данных коофициэнта
        {

            IEnumerable<Arhiv> Arhi = contexSlonik.arhivs.ToList();

            return Arhi;

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

        

        public Arhiv EditNazvanie(string Nazvanie)
        {
            throw new NotImplementedException();
        }

        public void Update(Arhiv entity)
        {
            throw new NotImplementedException();
        }

        public Detal Find(FindModel model)
        {
            throw new NotImplementedException();
        }

        public Arhiv Кosshot(int id)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}