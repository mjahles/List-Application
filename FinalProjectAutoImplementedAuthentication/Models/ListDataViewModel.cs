using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FinalProjectAutoImplementedAuthentication.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectAutoImplementedAuthentication.Models
{
    public class ListDataViewModel
    {
        public ApprovedUser ApprovedUser { get; set; }
        public ListInfo ListInfo { get; set; }
        public UserList UserList { get; set; }

        public List<ApprovedUser> ApprovedUsers { get; set; }
        public List<ListInfo> ListInfos { get; set; }
        public List<UserList> UserLists { get; set; }
    }
}