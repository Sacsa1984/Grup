using Slonik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slonik.Interfaces
{
    public interface ISlonik<T> where T : class
    {
        void Add(T detal);//добавить деталь
        T Кosshot(int id);//росчёт стоимости
        T EditNazvanie(string Nazvanie);//поиск по названию детали
        T EditOrder(int NumOrder);//поиск по номеру заказа
        T EditInd(int ind);//поиск по индексу
        IEnumerable<T> GetList();
        void Update(T entity);//редактировать
       
        void Delete(int id);//удаление по индексу
        Detal Find(FindModel model); //Поиск Ajax

    }
}
