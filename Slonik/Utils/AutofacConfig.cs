using Autofac;
using Autofac.Integration.Mvc;
using Slonik.Interfaces;
using Slonik.Models;
using Slonik.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slonik.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<RepositorySlonik>().As<ISlonik<Detal>>().SingleInstance();
            builder.RegisterType<RepositorCoefic>().As<ISlonik<Coefic>>();
            builder.RegisterType<RepositorArhiv>().As<ISlonik<Arhiv>>().SingleInstance();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}