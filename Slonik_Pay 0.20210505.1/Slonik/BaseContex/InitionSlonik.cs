using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Slonik.Models;

namespace Slonik.BaseContex
{
    public class InitionSlonik : DropCreateDatabaseAlways<ContexSlonik>
    {
        //заполнение таблиц тестовыми данными
        protected override void Seed(ContexSlonik context)
        {
            Coefic coefic1 = new Coefic { Id =1, Stanok= "Токарно-винторезный \"200mm\"", coeficientStoim=20, DopCoeficient=1,  stinostMM_proto=1, CoeficStanka=0.3, GlubMaxStan=4};
            Coefic coefic2 = new Coefic { Id = 2, Stanok = "Токарно-винторезный \"300mm\"", coeficientStoim = 20, DopCoeficient = 1, stinostMM_proto = 1, CoeficStanka = 0.3, GlubMaxStan = 4 };
            Coefic coefic3 = new Coefic { Id = 3, Stanok = "Токарно-винторезный \"400mm\"", coeficientStoim = 20, DopCoeficient = 1, stinostMM_proto = 1.2, CoeficStanka = 0.3, GlubMaxStan = 5 };
            Coefic coefic4 = new Coefic { Id = 4, Stanok = "Токарно-винторезный \"500mm\"", coeficientStoim = 20, DopCoeficient = 1, stinostMM_proto = 1.4, CoeficStanka = 0.3, GlubMaxStan = 6 };
            context.coefics.Add(coefic1);
            context.coefics.Add(coefic2);
            context.coefics.Add(coefic3);
            context.coefics.Add(coefic4);

            Detal detal1 = new Detal { Id = 1, StoimostDet=700, NumOrder = 000001, Nazvanie = "Вал L107", D =600, dO =0, WeithDetail = 7, Materia = "Сталь", Hurd = 35, CoeficId = 1, LDetal = 107, TypeObr = "Токарная", LProt = 107, Picture= "/Content/drawers/1364218129_1241-01.jpg",Glubina =15, Tochnost = 12 }; /*Tochnost по квалитету от 3-го до 14 чем выше точность меньше*/
            context.detals.Add(detal1);
            Detal detal2 = new Detal { Id = 2, StoimostDet = 1600, NumOrder = 000002, Nazvanie = "KL.710", D = 710, dO = 90, WeithDetail = 7, Materia = "Сталь", Hurd = 35, CoeficId = 1, LDetal = 107, TypeObr = "Токарная", LProt = 150, Picture = "/Content/drawers/4b90ef7dee328cbec625d82419a97bb3.jpg", Glubina = 20, Tochnost = 12 }; /*Tochnost по квалитету от 3-го до 14 чем выше точность меньше*/
            context.detals.Add(detal2);

            Users users1 = new Users { Id = 1, Key = "rG% \v", Name = "Петя", Dolgnost = "Admin", Role = "Admin", IsConnected = false };
            context.users.Add(users1);
            Users users2 = new Users { Id = 2, Key = "~B;=\0]", Name = "Вася", Dolgnost = "Master", Role = "Admin" , IsConnected = false};
            context.users.Add(users2);
            Arhiv arhiv = new Arhiv { Id = 1,  Material = "нет", Nazvanie = "нет", NumOrder = 0, Picture = "нет", Stanok = "нет", StoimostDet = 0, StoimostRashot = 0, TypeObr = "нет", WeithDetail = 0 };
            context.arhivs.Add(arhiv);

            base.Seed(context);
        }
    }
}