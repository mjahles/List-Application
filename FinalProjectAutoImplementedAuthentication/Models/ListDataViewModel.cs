using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectAutoImplementedAuthentication.Models;

namespace FinalProjectAutoImplementedAuthentication.Models
{
    public class ListDataViewModel
    {
        public List<ApprovedUser> ApprovedUsers { get; set; }
        public List<ListInfo> ListInfos { get; set; }
        public List<UserList> UserLists { get; set; }
    }
}