using Slonik.BaseContex;
using Slonik.Interfaces;
using Slonik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Slonik.Repository
{
    public class RepositorySlonik : ISlonik<Detal>, IDisposable
    {
        ContexSlonik contexSlonik;
        List<Detal> detalis;

        public RepositorySlonik()
        {
            contexSlonik = new ContexSlonik();
            detalis = contexSlonik.detals.ToList();
          
        }
        public void Add(Detal detal)//добавить деталь
        {
            contexSlonik.detals.Add(detal);
            contexSlonik.SaveChanges();

            detalis = contexSlonik.detals.ToList();
        }
        
        public Detal Кosshot(int id)//росчёт стоимости
        {
            double L = 0.0;//длина обрабатываемой поверхности
            double H = 0.0;//глубина обработки
            int HMaxStan = 0;//максимальная глубина мех обработки станка
            int ColProx = 0;//количество проходов 
            double DO = 0.0;//диаметыр обрабатываемой поверхности 
            double FaktProt = 0.0;
            double stoimMM = 0.0;//стоимость мм проточки
            double CofT = 0.0;//кофициэнт точности
            double sta = 0.0;//кофициэнт станка
            double DopCof = 0.0;//Дополнительный кофициэнт
           Detal deta = contexSlonik.detals.Include(item=>item.Coefic).FirstOrDefault(item => item.Id == id);
           
            L = deta.LProt;
            H = deta.Glubina;
            HMaxStan =deta.Coefic.GlubMaxStan;
            stoimMM = deta.Coefic.stinostMM_proto;
            DO = deta.dO * 3.14;
            CofT = (16-deta.Tochnost)/2;
            sta = deta.Coefic.CoeficStanka;
            DopCof = deta.Coefic.DopCoeficient;
            if (H % HMaxStan == 0)
            {
                if (H/HMaxStan<=3)
                {
                    ColProx = Convert.ToInt32(H / HMaxStan);//если делится без остатка оставляем фактические данные 
                }
                else
                {
                    ColProx = Convert.ToInt32((H / HMaxStan) * 0.7);
                }
                
            }
            else
            {
                if (H / HMaxStan <= 3)
                {
                    ColProx = Convert.ToInt32(H / HMaxStan) + 1;//если с остатком увеличиваем на один
                }
                if (H / HMaxStan == 0)
                {
                    ColProx = 1;
                }
                else
                {
                    ColProx = Convert.ToInt32((H / HMaxStan) * 0.7);
                }

            }
            FaktProt = L* ColProx;//получяем фактическую длину обработки с учётом припуска
           
            var items = contexSlonik.detals.FirstOrDefault(item=>item.Id==id);


            items.StoimostRashot = (FaktProt* DO / 1000) * stoimMM * CofT* sta* DopCof;
            contexSlonik.Entry(items).State = EntityState.Modified;//обновить данные таблици 
         
            contexSlonik.SaveChanges();
            return items;
        }



        public Detal EditOrder(int NumOrder)//поиск по номеру заказа
        {
            return contexSlonik.detals.FirstOrDefault(item => item.NumOrder == NumOrder);
        }
        public Detal EditNazvanie(string Nazvanie)//поиск по номеру заказа
        {
            return contexSlonik.detals.FirstOrDefault(item => item.Nazvanie == Nazvanie);
        }
        public Detal EditInd(int ind)
        {
            return contexSlonik.detals.FirstOrDefault(item => item.Id == ind);
        }

       

       

        public void Edit(string Nazvanie)
        {
            throw new NotImplementedException();
        }

       

        public IEnumerable<Detal> GetList()//возврат данных коофициэнта
        {
            
                IEnumerable<Detal> detals = contexSlonik.detals.Include(x => x.Coefic).ToList();
            
                return detals;
           
        }
        //public IEnumerable<Detal> GetListUsers()
        //{
        //    IEnumerable<Detal> users = contexSlonik.detals.Include(x => x.Users).ToList();
        //    return users;
        //}

        public void Add(Users detal)
        {
            throw new NotImplementedException();
        }

        public  Detal Find(FindModel model) //Поиск по Ajax запросу
        {
            
           return  detalis.Where(x => x.Nazvanie == model.Nazvanie || x.NumOrder == model.NumOrder).FirstOrDefault();
        }
        public void Delete(int id)
        {

            contexSlonik.detals.Remove(contexSlonik.detals.Find(id));
            contexSlonik.SaveChanges();
            detalis = contexSlonik.detals.ToList();
        }

        public void Update(Detal entity)
        {

            int index = contexSlonik.detals.FirstOrDefault(x => x.Id == entity.Id).Id;
            var item = contexSlonik.detals.Find(index);


            item.D = entity.D;
            item.LDetal = entity.LDetal;
            item.Materia = entity.Materia;
            item.Hurd = entity.Hurd;
            item.StoimostDet = entity.StoimostDet;
            item.dO = entity.dO;
            item.LProt = entity.LProt;
            item.Glubina = entity.Glubina;

            contexSlonik.SaveChanges();
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



        /* public IEnumerable<Detal> GetList(int NumOrder)
{
throw new NotImplementedException();
}

public IEnumerable<Detal> GetList(string Nazvanie)
{
throw new NotImplementedException();
}*/
    }
}