using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectAutoImplementedAuthentication.Models;
using FinalProjectAutoImplementedAuthentication.Abstract;
using FinalProjectAutoImplementedAuthentication.Entities;
using Microsoft.AspNet.Identity;

namespace FinalProjectAutoImplementedAuthentication.Controllers
{
    public class ListController : Controller
    {
        private IUserListRespository repository;

        public ListController(IUserListRespository repo)
        {
            repository = repo;
        }

        public ListController()
        {
          
        }

        ListDataEntities DB = new ListDataEntities();
        // GET: List
        [Authorize]
        public ActionResult IndexList()
        {
            List<ApprovedUser> approvedUsers = DB.ApprovedUsers.ToList();
            List<ListInfo> listInfos = DB.ListInfoes.ToList();
            List<Models.UserList> userLists = DB.UserLists.ToList();

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
            List<Models.UserList> userLists = DB.UserLists.ToList();

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

        [HttpGet]
        public ActionResult Create()
        {
            ViewData["userid"] = User.Identity.GetUserId();
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Models.UserList userList)
        {
            ViewData["userid"] = User.Identity.GetUserId();

            try
            {
                if (userList != null)
                {
                    Models.UserList listData = new Models.UserList();
                    listData.ListId = userList.ListId;
                    listData.ListName = userList.ListName;
                    listData.RowCount = userList.RowCount;
                    listData.ColumnCount = userList.ColumnCount;
                    listData.OwnerId = ViewData["userid"].ToString();

                    DB.UserLists.Add(listData);
                    DB.SaveChanges();
                }
                return RedirectToAction("IndexList");
            }
            catch (Exception)
            {
                ViewBag.msg = "An error has occured.";
                return RedirectToAction("IndexList");
            }
        }

        public ViewResult EditList(int listId)
        {
            ViewData["userid"] = User.Identity.GetUserId();
            ViewBag.Heading = "Edit List";

            List<Models.UserList> repositoryUserLists = DB.UserLists.ToList();

            //ViewData["selectedRecord"] = listId;

            Models.UserList editableUserList = repositoryUserLists
                .FirstOrDefault(x => x.ListId == listId);
            return View(editableUserList); //This renders for some reason. Doesn't save changes though
        }

        [HttpPost]
        public ActionResult EditList(Models.UserList userList)
        {
            if (ModelState.IsValid)
            {
                Models.UserList listDataEntry = DB.UserLists.Find(userList.ListId);

                if (listDataEntry != null)
                {
                    listDataEntry.ListId = userList.ListId;
                    listDataEntry.ListName = userList.ListName;
                    listDataEntry.RowCount = userList.RowCount;
                    listDataEntry.ColumnCount = userList.ColumnCount;
                    listDataEntry.OwnerId = userList.OwnerId;
                }
                DB.SaveChanges();

                TempData["message"] = string.Format("{0} has been saved", userList.ListName);

                return RedirectToAction("IndexList");
            }

            else
            {
                // This code is executed if something is wrong with the data values
                return View(userList);
            }
        }

        public ActionResult DeleteList()
        {
            return View();
        }
    }
}