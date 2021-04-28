using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FinalProjectAutoImplementedAuthentication.Controllers
{
    public class HomeController : Controller
    {
        //UserDataEntity DB = new UserDataEntity();

        public ActionResult Index()
        {


            return View();
        }

        /*public ActionResult Index1()
        {

            List<UserList> UserList = DB.UserLists.ToList();
            return View(UserList);
        }*/

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}