using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectAutoImplementedAuthentication.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectAutoImplementedAuthentication.Models
{
    public class EditListInfoViewModel
    {
        //[Key]
        public ListInfo ListInfo { get; set; }
        public List<ListInfo> ListInfos { get; set; }
    }
}