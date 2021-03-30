using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectAutoImplementedAuthentication.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        //[Authorize]
        public ActionResult IndexList()
        {
            return View();
        }

        public ActionResult MyLists()
        {
            return View();
        }

        public ActionResult CreateList()
        {
            return View();
        }
    }
}