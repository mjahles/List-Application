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

        [HttpGet]
        public ActionResult Create()
        {
            ViewData["userid"] = User.Identity.GetUserId();
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(UserList userList)
        {
            ViewData["userid"] = User.Identity.GetUserId();

            try
            {
                if (userList != null)
                {
                    UserList listData = new UserList();
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

            List<UserList> repositoryUserLists = DB.UserLists.ToList();

            //ViewData["selectedRecord"] = listId;

            UserList editableUserList = repositoryUserLists
                .FirstOrDefault(x => x.ListId == listId);
            return View(editableUserList);
        }

        [HttpPost]
        public ActionResult EditList(UserList userList)
        {
            if (ModelState.IsValid)
            {
                UserList listDataEntry = DB.UserLists.Find(userList.ListId);

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

        [HttpGet]
        public ActionResult DeleteList(int listId)
        {
            UserList deletedEntry = DB.UserLists.Find(listId);

            return View(deletedEntry);
        }

        [HttpPost]
        public ActionResult DeleteList(UserList listDataEntry) //Takes in the ListId as a parameter to locate the list that is to be deleted.
        {
            UserList deletedEntry = DB.UserLists.Find(listDataEntry.ListId);

            if (deletedEntry.OwnerId == User.Identity.GetUserId())
            {
                if (deletedEntry != null) //Checking that the list exists
                {
                    DB.UserLists.Remove(deletedEntry);
                    DB.SaveChanges();
                }
            }
            return RedirectToAction("IndexList");
        }

        [HttpGet]
        public ActionResult EditListInfo(int listId, string message)
        {
            UserList userList = DB.UserLists.Find(listId);

            ViewData["listId"] = listId;
            ViewData["rowCount"] = userList.RowCount;
            ViewData["columnCount"] = userList.ColumnCount;
            ViewData["message"] = message;
            ViewBag.Heading = listId;
            List<ListInfo> listInfos = new List<ListInfo>();

            foreach (var info in DB.ListInfoes.ToList().Where(x => x.ListId == listId))
            {
                listInfos.Add(info);
            }

            IEnumerable<ListInfo> listData = listInfos;

            EditListInfoViewModel modelInput = new EditListInfoViewModel()
            {
                ListInfos = listData.ToList()
            };

            //List<ApprovedUser> approvedUsers = DB.ApprovedUsers.ToList();
            //List<ListInfo> listInfos = DB.ListInfoes.ToList();
            //List<UserList> userLists = DB.UserLists.ToList();

            /*ListDataViewModel model = new ListDataViewModel()
            {
                ApprovedUsers = approvedUsers,
                ListInfos = listInfos,
                UserLists = userLists
            };

            return View(model);*/
            return View(listData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditListInfo(List<ListInfo> modelData)
        {
            string message;

            if (ModelState.IsValid)
            {
                if (modelData != null)
                {
                    foreach (var info in modelData)
                    {
                        //if (info.ListId == (int)ViewData["idChecker"])
                        {
                            ListInfo dbRecord = DB.ListInfoes.Find(info.InfoId);

                            if (dbRecord != null)
                            {
                                dbRecord.InfoId = info.InfoId;
                                dbRecord.ColumnData = info.ColumnData;
                                dbRecord.RowNum = info.RowNum;
                                dbRecord.ColumnNum = info.ColumnNum;
                                dbRecord.ListId = info.ListId;
                                dbRecord.IsChecked = info.IsChecked;
                            }
                            if (dbRecord == null)
                            {
                                DB.ListInfoes.Add(info);
                            }
                        }
                    }
                    DB.SaveChanges();
                }
                message = "Changes Saved Successfully";
                return RedirectToAction("EditListInfo", new { modelData.FirstOrDefault().ListId, message });
            }
            message = "An error has occured. Please try again";
            return RedirectToAction("EditListInfo", new { modelData.FirstOrDefault().ListId, message });
        }

        public ActionResult DeleteListInfo(int listId)
        {
            List<ListInfo> deletedListInfo = new List<ListInfo>();

            foreach (var entry in DB.ListInfoes)
            {
                if (listId == entry.ListId)
                {
                    DB.ListInfoes.Remove(entry);
                }
            }
            DB.SaveChanges();

            return RedirectToAction("IndexList");
        }

        [HttpGet]
        public ActionResult ShareList(int listId)
        {
            ViewData["listId"] = listId;
            return PartialView("ShareList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareList(int listId, string searchedEmail)
        {
            string message;

            if (ModelState.IsValid)
            {
                string userId;
                List<AspNetUser> userList = new List<AspNetUser>();

                foreach (var userRecord in DB.AspNetUsers)
                {
                    if (userRecord.Email == searchedEmail)
                    {
                        userList.Add(userRecord);
                    }
                }

                userId = userList.FirstOrDefault().Id;

                if (userId != null)
                {
                    foreach (var appUser in DB.ApprovedUsers)
                    {
                        if (appUser.UserId == userId && appUser.ListId == listId)
                        {
                            message = "That user already has access to this list.";
                            return RedirectToAction("EditListInfo", new { listId, message } );
                        }
                    }

                    ApprovedUser userEntry = new ApprovedUser();
                    userEntry.UserId = userId;
                    userEntry.ListId = listId;

                    DB.ApprovedUsers.Add(userEntry);
                    DB.SaveChanges();

                    message = "User successfully added.";
                    return RedirectToAction("EditListInfo", new { listId, message });
                }
            }
            message = "An error has occured. Please try again.";
            return RedirectToAction("EditListInfo", new { listId, message });
        }

        public ActionResult DeleteAllSharedUsers(int listId)
        {
            List<ApprovedUser> deletedApprovedUsers = new List<ApprovedUser>();

            foreach (var user in DB.ApprovedUsers)
            {
                if (listId == user.ListId)
                {
                    DB.ApprovedUsers.Remove(user);
                }
            }
            DB.SaveChanges();

            return RedirectToAction("IndexList");
        }

        public ActionResult CreateRows(UserList userList)
        {
            var rowNumber = 1;
            var columnNumber = 1;

            while (rowNumber <= userList.RowCount)
            {
                while (columnNumber <= userList.ColumnCount)
                {
                    ListInfo listInfo = new ListInfo();
                    listInfo.ColumnData = "";
                    listInfo.RowNum = rowNumber;
                    listInfo.ColumnNum = columnNumber;
                    listInfo.ListId = userList.ListId;
                    listInfo.IsChecked = false;

                    DB.ListInfoes.Add(listInfo);

                    columnNumber++;
                }
                rowNumber++;
                columnNumber = 1;
            }
            DB.SaveChanges();
            return RedirectToAction("IndexList");
        }

    }
}