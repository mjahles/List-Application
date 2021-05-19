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
            ViewBag.Heading = userList.ListName;
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

        [HttpGet]
        public ActionResult ManageRowColumn(int listId)
        {
            UserList model = DB.UserLists.Find(listId);

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRowColumn(UserList userList)
        {
            string message;

            if (ModelState.IsValid)
            {
                // Fixing the indexing with the below statements. I add 1 to the values so that the user can easily know how many rows and columns their table should have. Then I subtract 1 below to correct the value back to what it should be in the database.
                userList.RowCount = userList.RowCount - 1;
                userList.ColumnCount = userList.ColumnCount - 1;

                UserList listEntry = DB.UserLists.Find(userList.ListId);
                UserList oldListValues = new UserList();

                oldListValues.ListId = listEntry.ListId;
                oldListValues.RowCount = listEntry.RowCount;
                oldListValues.ColumnCount = listEntry.ColumnCount;

                listEntry.ListId = userList.ListId;
                listEntry.RowCount = userList.RowCount;
                listEntry.ColumnCount = userList.ColumnCount;

                DB.SaveChanges();
                
                if (listEntry.RowCount < oldListValues.RowCount)
                {
                    List<ListInfo> listInfos = new List<ListInfo>();

                    foreach (var info in DB.ListInfoes)
                    {
                        if (info.ListId == userList.ListId && info.RowNum >= oldListValues.RowCount)
                        {
                            listInfos.Add(info);
                        }
                    }

                    DB.ListInfoes.RemoveRange(listInfos);
                }

                if (listEntry.ColumnCount < oldListValues.ColumnCount)
                {
                    List<ListInfo> listInfos = new List<ListInfo>();

                    foreach (var info in DB.ListInfoes)
                    {
                        if (info.ListId == userList.ListId && info.ColumnNum >= oldListValues.ColumnCount)
                        {
                            listInfos.Add(info);
                        }
                    }

                    DB.ListInfoes.RemoveRange(listInfos);
                }

                if (listEntry.RowCount > oldListValues.RowCount)
                {
                    List<ListInfo> infoEntries = new List<ListInfo>();
                    int rowDifference = listEntry.RowCount - oldListValues.RowCount;
                    int rowCounter = 0;
                    int columnCounter = 0;

                    while (rowCounter <= rowDifference)
                    {
                        while (columnCounter <= userList.ColumnCount)
                        {
                            ListInfo entry = new ListInfo();
                            entry.ColumnData = "";
                            entry.RowNum = listEntry.RowCount - rowCounter;
                            entry.ColumnNum = columnCounter;
                            entry.ListId = userList.ListId;
                            columnCounter++;

                            infoEntries.Add(entry);
                        }
                        rowCounter++;
                        columnCounter = 0;
                    }
                    DB.ListInfoes.AddRange(infoEntries);
                }

                if (listEntry.ColumnCount > oldListValues.ColumnCount)
                {
                    List<ListInfo> infoEntries = new List<ListInfo>();
                    List<ListInfo> infoData = new List<ListInfo>();
                    int columnDifference = listEntry.ColumnCount - oldListValues.ColumnCount;
                    int rowCounter = 0;
                    int columnCounter = 0;

                    foreach (var info in DB.ListInfoes)
                    {
                        if (info.ListId == userList.ListId)
                        {
                            infoData.Add(info);
                        }
                    }

                    while (columnCounter < columnDifference)
                    {
                        while (rowCounter <= userList.RowCount)
                        {
                            ListInfo entry = new ListInfo();
                            entry.ColumnData = "";
                            entry.RowNum = rowCounter;
                            entry.ColumnNum = listEntry.ColumnCount - columnCounter;
                            entry.ListId = userList.ListId;

                            foreach (var item in infoData)
                            {
                                if (item.RowNum == entry.RowNum)
                                {
                                    if (item.IsChecked == true)
                                    {
                                        entry.IsChecked = item.IsChecked;
                                        break;
                                    }
                                }
                            }
                            rowCounter++;

                            infoEntries.Add(entry);
                        }
                        columnCounter++;
                        rowCounter = 0;
                    }
                    DB.ListInfoes.AddRange(infoEntries);
                }
                DB.SaveChanges();
                message = "List Updated";
                return RedirectToAction("EditListInfo", new { userList.ListId, message });
            }
            message = "An error has occurred. Please try again.";
            return RedirectToAction("EditListInfo", new { userList.ListId, message });
        }

        public ActionResult CreateDefaultInfo(UserList userList)
        {
            string message;
            int rowNumber = 0;
            int columnNumber = 0;

            foreach (var info in DB.ListInfoes)
            {
                if (info.ListId == userList.ListId)
                {
                    message = null;
                    return RedirectToAction("EditListInfo", new { userList.ListId, message });
                }
            }

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
                columnNumber = 0;
            }
            DB.SaveChanges();
            message = null;
            return RedirectToAction("EditListInfo", new { userList.ListId, message });
        }

        [HttpGet]
        public ActionResult ViewSharedList(int listId, string message)
        {
            UserList userList = DB.UserLists.Find(listId);
            ViewData["listId"] = userList.ListId;
            ViewData["rowCount"] = userList.RowCount;
            ViewData["columnCount"] = userList.ColumnCount;
            ViewData["message"] = message;
            ViewBag.Heading = userList.ListName;

            List<ListInfo> listInfos = new List<ListInfo>();

            IEnumerable<ListInfo> listData = listInfos;

            foreach (var info in DB.ListInfoes)
            {
                if (info.ListId == listId)
                {
                    listInfos.Add(info);
                }
            }
            return View(listData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewSharedList(List<ListInfo> modelData)
        {
            string message;

            if (ModelState.IsValid)
            {
                if (modelData != null)
                {
                    foreach (var info in modelData)
                    {
                        ListInfo dbRecord = DB.ListInfoes.Find(info.InfoId);

                        dbRecord.IsChecked = info.IsChecked;

                        if (info.IsChecked == true)
                        {
                            foreach (var item in modelData)
                            {
                                if (item.RowNum == info.RowNum)
                                {
                                    ListInfo subRecord = DB.ListInfoes.Find(item.InfoId);

                                    subRecord.IsChecked = info.IsChecked;
                                }
                            }
                        }
                    }
                    DB.SaveChanges();
                }
                message = "Changes Saved Successfully";
                return RedirectToAction("ViewSharedList", new { modelData.FirstOrDefault().ListId, message });
            }
            message = "An Error has occured. Please try again.";
            return RedirectToAction("ViewSharedList", new { modelData.FirstOrDefault().ListId, message });
        }

    }
}