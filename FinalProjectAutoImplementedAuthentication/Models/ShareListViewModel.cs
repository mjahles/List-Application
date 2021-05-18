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
        public AspNetUser AspNetUser { get; set; }
        public ApprovedUser ApprovedUser { get; set; }
    }
}