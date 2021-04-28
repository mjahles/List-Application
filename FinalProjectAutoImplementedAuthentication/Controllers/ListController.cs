using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectAutoImplementedAuthentication.Models;

namespace FinalProjectAutoImplementedAuthentication.Controllers
{
    public class ListController : Controller
    {
        UserListsEntities DB = new UserListsEntities();
        // GET: List
        [Authorize]
        public ActionResult IndexList()
        {
            List<UserList> userLists = DB.UserLists.ToList();
            return View(userLists);
        }

        public ActionResult MyLists()
        {
            List<UserList> userLists = DB.UserLists.ToList();
            return View(userLists);
        }

        //You have to render a data list in both the partial view and the containing view in order for it to work. I had to setup the UserLists data to render in IndexList() in order for it to render in MyLists().  If I don't then I get a NullReferenceException.

        public ActionResult CreateList()
        {
            return View();
        }

        public ActionResult DeleteList()
        {
            return View();
        }
    }
}