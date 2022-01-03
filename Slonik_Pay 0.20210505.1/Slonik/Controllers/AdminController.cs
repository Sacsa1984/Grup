using Slonik.BaseContex;
using Slonik.Interfaces;
using Slonik.Models;
using Slonik.Repository;
using Slonik.ViveModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slonik.Controllers
{
    public class AdminController : Controller
    {
        ISlonik<Detal> slonik=null;
        ISlonik<Coefic> coefRep;
        ISlonik<Arhiv> Arhiv1;

        Security security = new Security();
        ContexSlonik contexSlonik1 = new ContexSlonik();

        string PageUser = "Admin";

        public AdminController(ISlonik<Detal> slonikObj, ISlonik<Coefic> coefRep, ISlonik<Arhiv> Arhivobj)
        {
            this.slonik = slonikObj;
            this.coefRep = coefRep;
            this.Arhiv1 = Arhivobj;
        }

        public ActionResult Index()
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View();
            }
            else { return RedirectToAction("IndexStart","Home"); }
        }

        public ActionResult AddDetail()//Добавить деталь
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                SelectList list = new SelectList(coefRep.GetList(), "Id", "Stanok");
                ViewBag.list = list;
                return View();
            }
            else { return RedirectToAction("IndexStart", "Home"); }
        }

        [HttpGet]
        public ActionResult ShowAll(int page = 1)//Показать все детали 
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                int pageSize = 3; // количество объектов на странице. Ставите на своё усмотрение
                IEnumerable<Detal> detals = slonik.GetList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = slonik.GetList().Count() };

                IndexViewModel viewModel = new IndexViewModel { Detals = detals, PageInfo = pageInfo };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }

        [HttpGet]
        public ActionResult FindDetail() //Окно поиска
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View();
            }
            else { return RedirectToAction("IndexStart", "Home"); }
        }
        [HttpPost]
        public ActionResult ShowDetal(FindModel model)//Вывод окна результата поиска
        {
            Detal result = slonik.Find(model);

            if (result != null) { return View(result); }
            else { return null; }
        }

        public ActionResult ShouDet(Detal detal)//Вывод окна результата поиска
        {if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View("ShouDet", slonik.EditOrder(detal.NumOrder));
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }

        public ActionResult LastOrder()//Последний заказ
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                IEnumerable<Detal> detals = slonik.GetList();
                IndexViewModel viewModel = new IndexViewModel { Detals = detals };

                return View(viewModel);
            }
            else { return RedirectToAction("IndexStart", "Home"); }
        }

        public ActionResult Archive()//Архив
        {if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View();
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }

        public ActionResult AddFacilities()//Добавить оборудование
        {if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View();
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }
        [HttpPost]
        public ActionResult Add(AddDetalChertViewModel viewModel)
        {

            coefRep.Add(viewModel.detalPik.Coefic);
            return View("Index");
        }

        [HttpGet]
        public ActionResult AddDetal()
        {if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                AddDetalChertViewModel vm = new AddDetalChertViewModel() { detalPik = new Detal() };

                



                return View(vm);
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddAdmin(AddDetalChertViewModel viewModel)
        {
            if (viewModel != null)
            {

               




                string fileName;
                int prip = 0;

                fileName = Path.GetFileName(viewModel.File.FileName);//добавляем чертёж 
                string path = Path.Combine(Server.MapPath("~/Content/drawers"), fileName);
                viewModel.File.SaveAs(path);
                viewModel.detalPik.Picture = "/Content/drawers/" + fileName;

                Arhiv arhiv = new Arhiv();

                arhiv.Material = viewModel.detalPik.Materia;
                arhiv.Nazvanie = viewModel.detalPik.Nazvanie;
                arhiv.NumOrder = viewModel.detalPik.NumOrder;
                arhiv.Picture = viewModel.detalPik.Picture;
                arhiv.TypeObr = viewModel.detalPik.TypeObr;
                arhiv.StoimostDet = viewModel.detalPik.StoimostDet;
                arhiv.WeithDetail = viewModel.detalPik.WeithDetail;
                

                foreach (int item in viewModel.IntList_L)//сложение длин поясков 
                {
                    viewModel.detalPik.LProt += item;
                    prip++;
                }

                foreach (int item in viewModel.IntList_T)//высчитываем средний припуск
                {
                    viewModel.detalPik.Glubina += item;

                }

                viewModel.detalPik.Glubina /= prip;


                foreach (double item in viewModel.IntList_D)//высчитываем средний диаметыр
                {

                    viewModel.detalPik.dO += item;

                }
                foreach (int item in viewModel.IntList_TocHnost)//высчитываем средней точности
                {

                    viewModel.detalPik.Tochnost += item;

                }
                viewModel.detalPik.Tochnost /= prip;

                IEnumerable<Coefic> coefics = contexSlonik1.coefics.ToList();

                foreach (Coefic item in coefics)
                {
                    if (viewModel.detalPik.CoeficId == item.Id)//поиск нужного оборудования
                    {
                        Detal delet = new Detal();
                        Coefic coefic = new Coefic();
                        slonik.Add(viewModel.detalPik);
                        slonik.Кosshot(viewModel.detalPik.Id);
                        
                        delet.StoimostRashot= slonik.Кosshot(viewModel.detalPik.Id).StoimostRashot;
                        coefic.Stanok = slonik.Кosshot(viewModel.detalPik.Id).Coefic.Stanok;
                        arhiv.StoimostRashot = delet.StoimostRashot;
                        arhiv.Stanok = coefic.Stanok;
                        Arhiv1.Add(arhiv);
                        return RedirectToAction("ShouDet", slonik.EditOrder(viewModel.detalPik.NumOrder));
                    }
                }
               
                return RedirectToAction("Home","Error");
            }
            else
            {
                return RedirectToAction("Home", "Error");
            }





        }
        public ActionResult Error()//Вывод страници ошибок
        {
            return View("Home", "Error");
        }

        public ActionResult Redakt(int id)//редактировать
        {
                return View(slonik.EditInd(id));
 
            
        }

        public ActionResult Delet(int Id)
        {
            slonik.Delete(Id);
            return RedirectToAction("Index");
        }

        public ActionResult ShowObor()//Показать все детали 
        {if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                IEnumerable<Coefic> Coef = coefRep.GetList();
                IndexViewModel viewModel = new IndexViewModel { Coefic = Coef };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }
        [HttpPost]
        public ActionResult Zame(Detal entity)
        {
            slonik.Update(entity);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult ShowAllArhiv(int page = 1)//Показать все детали 
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {

                int pageSize = 3; // количество объектов на странице. Ставите на своё усмотрение
                IEnumerable<Arhiv> arhiv = Arhiv1.GetList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = slonik.GetList().Count() };

                IndexViewModel viewModel1 = new IndexViewModel { Arhiv = arhiv, PageInfo = pageInfo };
                return View(viewModel1);
               

                
                
            }
            else
            {
                return RedirectToAction("IndexStart", "Home");
            }
        }

        public ActionResult Team() 
        {
            return View();
        }
    }
}
