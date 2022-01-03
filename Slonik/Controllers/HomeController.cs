using Slonik.BaseContex;
using Slonik.Interfaces;
using Slonik.Models;
using Slonik.Repository;
using Slonik.ViveModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Slonik.Controllers
{
    public class HomeController : Controller
    {
        ISlonik<Detal> slonik;
        ISlonik<Coefic> coefRep;
        ISlonik<Arhiv> Arhiv1;
        Security security = new Security(); //Этот класс отвечает за безопасность. За шифровку/дешифровку
        ContexSlonik contexSlonik1=new ContexSlonik();

        string PageUser = "home"; //Должность пользователя, для которого предполагается страница

        public HomeController(ISlonik<Detal> slonikObj, ISlonik<Coefic> coefRep, ISlonik<Arhiv> Arhivobj)
        {
            this.slonik = slonikObj;
            this.coefRep = coefRep;
            this.Arhiv1 = Arhivobj;
        }
       
        [HttpGet]
        public ActionResult IndexStart()
        {
          
            try
            {
                //Здесь выскакивает исключение, оповещающее, что куки нет. Пропускаем его, оно не мешает работе программы, так как куки инициализируются после авторизации

                HttpCookie req = Request.Cookies["User"]; //Получаем доступ к куки
                if (req["Position"] != null && req["Position"] != "none") //Если куки есть
                {
                    req["Position"] = "none";
                    req.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(req);
                }
            }
            catch (Exception) { }

            return View("IndexStart");
        }

        //Каждый метод GET первым делом проверяет наличие куки
        //Если куки нет, или они уже не действительны, то пользователя перебрасывет на IndexStart

        public ActionResult Index()
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser)) 
            {
                return View();
            }
            else { return RedirectToAction("IndexStart"); }
        }

        [HttpPost]
        public ActionResult Polzow(string Login, string Pass) //Передавать объект класса User теперь не нужно, так как User больше не имеет параметрова Login и Password
        {                                                     //Для авторизации User имеет параметр Key, который хранит зашифрованные Login и Password
            IEnumerable<Users> usersCol = contexSlonik1.users.ToList();

            if(Login == "" || Pass == "") { return View("IndexStart"); }

           // var encrypt = security.Encrypt(Login, Pass); //Собственно, так происходит зашифровка пароля. По Login и Password который ввёл пользователь шифруется его Key

            foreach (Users item in usersCol)
            {
                if (security.Decrypt(item.Key,Pass) == Login) //Если логин и пароль введены прваильно, то дешифратор разшифрует Key, и на исходе мы получим такой же логин, который был введён пользователем в поле Login
                {
         
                    //После каждой авторизации создаются куки с должностью 

                    if ("Admin"== item.Dolgnost)
                    {
                        HttpCookie cookie = new HttpCookie("User");
                        cookie["Position"] = "admin";
                        cookie.Expires = DateTime.Now.AddHours(1); //Пользователь может сидеть час под аккаунтом
                        cookie.Secure = true;
                        Response.Cookies.Add(cookie);

                        IEnumerable<Detal> detals = slonik.GetList();
                        IndexViewModel viewModel = new IndexViewModel { Detals = detals };
                        
                        return RedirectToAction("Index", "Admin", viewModel);
                    }
                    if ("Master"== item.Dolgnost)
                    {
                        HttpCookie cookie = new HttpCookie("User");
                        cookie["Position"] = "home";
                        cookie.Expires = DateTime.Now.AddHours(1);
                        cookie.Secure = true;
                        Response.Cookies.Add(cookie);

                        IEnumerable<Detal> detals = slonik.GetList();
                        IndexViewModel viewModel = new IndexViewModel { Detals = detals };
                        return RedirectToActionPermanent("Index", viewModel);
                    }
                    else
                    {
                        
                        return View("IndexStart");
                    }
                }

            }
            return View("IndexStart");
        }
        public ActionResult AddDetals()
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                SelectList list = new SelectList(coefRep.GetList(), "Id", "Stanok");
                ViewBag.list = list;
                return View();
            }
            else { return RedirectToAction("IndexStart"); }
        }

        [HttpGet]
        public ActionResult AddDetal()
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                AddDetalChertViewModel vm = new AddDetalChertViewModel() { detalPik = new Detal() };
                return View(vm);
            }
            else { return RedirectToAction("IndexStart"); }
        }

        [HttpPost]
        public ActionResult Add( AddDetalChertViewModel viewModel)
        {
            if (viewModel!=null)
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
                        delet.StoimostRashot = slonik.Кosshot(viewModel.detalPik.Id).StoimostRashot;
                        coefic.Stanok = slonik.Кosshot(viewModel.detalPik.Id).Coefic.Stanok;
                        arhiv.StoimostRashot = delet.StoimostRashot;
                        arhiv.Stanok = coefic.Stanok;
                        Arhiv1.Add(arhiv);

                        return RedirectToAction("ShouDet", slonik.EditOrder(viewModel.detalPik.NumOrder));
                }
            }

                return RedirectToAction("Error");
            }
            else
            {
                return RedirectToAction("Error");
            }
           
        }
        public ActionResult Error()//Вывод страници ошибок
        {
            return View("Error");
        }
        public ActionResult ShouDet(Detal detal)//Вывод окна результата поиска
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View("ShouDet", slonik.EditOrder(detal.NumOrder));
            }
            else { return RedirectToAction("IndexStart"); }
        }
        public ActionResult ShowAll(int page = 1)//Показать все детали 
        { //В переменную page помещается выбранная страница. По-умолчанию - это первая страница
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                int pageSize = 9; // количество объектов на странице. Ставите на своё усмотрение
                IEnumerable<Detal> detals = slonik.GetList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = slonik.GetList().Count() };

                IndexViewModel viewModel = new IndexViewModel { Detals = detals, PageInfo = pageInfo };
                return View(viewModel);
            }
            else { return RedirectToAction("IndexStart"); }
        }

        [HttpGet]
        public ActionResult FindDetail() //Окно поиска
        {
            if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                return View();
            }
            else { return RedirectToAction("IndexStart"); }
        }
        [HttpPost]
        public /*async Task*/  ActionResult ShowDetal(FindModel model)//Вывод окна результата поиска
        {
            if (model!=null)
            {
                Detal result = /*await Task.Run(() =>*/ slonik.Find(model)/*)*/;

                /*slonik.Кosshot(result.Id);*/
                if (result != null) { return View(result); }
                else { return null; }
            }
            else
            {
                return  View("Error");
            }

           
        }
        public ActionResult LastOrder()//Последний заказ
        {if (security.CheckCookie(Request.Cookies["User"], PageUser))
            {
                IEnumerable<Detal> detals = slonik.GetList();
                IndexViewModel viewModel = new IndexViewModel { Detals = detals };
                return View(viewModel);
            }
            else
            {
               return RedirectToAction("IndexStart");
            }
            
        }

        public ActionResult Team()
        {
            return View();
        }
    }
}