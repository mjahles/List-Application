using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectAutoImplementedAuthentication.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectAutoImplementedAuthentication.Models
{
    public class ShareListViewModel
    {
        [Key]
        public ListInfo listInfo { get; set; }
        public AspNetUser aspNetUser { get; set; }
        public ApprovedUser approvedUser { get; set; }
    }
}