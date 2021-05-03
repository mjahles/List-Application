using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectAutoImplementedAuthentication.Models;
using Microsoft.AspNet.Identity;

namespace FinalProjectAutoImplementedAuthentication.Controllers
{
    public class ListController : Controller
    {
        ListDataEntities DB = new ListDataEntities();
        // GET: List
        [Authorize]
        public ActionResult IndexList()
        {
            List<ApprovedUser> approvedUsers = DB.ApprovedUsers.ToList();
            List<ListInfo> listInfos = DB.ListInfoes.ToList();
            List<UserList> userLists = DB.UserLists.ToList();

            ListDataViewModel model = new ListDataViewModel()
            {
                ApprovedUsers = approvedUsers,
                ListInfos = listInfos,
                UserLists = userLists
            };
            
            ViewData["userid"] = User.Identity.GetUserId();

            return View(model);
        }

        public ActionResult MyLists()
        {
            //List<UserList> userLists = DB.UserLists.ToList();
            //return View(userLists);
            //return View();
            List<ApprovedUser> approvedUsers = DB.ApprovedUsers.ToList();
            List<ListInfo> listInfos = DB.ListInfoes.ToList();
            List<UserList> userLists = DB.UserLists.ToList();

            ListDataViewModel model = new ListDataViewModel()
            {
                ApprovedUsers = approvedUsers,
                ListInfos = listInfos,
                UserLists = userLists
            };
            
            ViewData["userid"] = User.Identity.GetUserId();

            return View(model);
        }

        //You can pass view data from a containing view to a partial view that is rendered within the containing view. I can display the ListData from IndexList() inside of MyLists() since MyLists() is rendered within IndexList(). The same is true for ViewData[].

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