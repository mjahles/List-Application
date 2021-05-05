using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectAutoImplementedAuthentication.Entities
{
    public class UserList
    {
        [HiddenInput (DisplayValue = false)]
        [Key]
        public int ListId { get; set; }
        
        [Required (ErrorMessage = "Please enter a list name")]
        public string ListName { get; set; }
        
        [Required (ErrorMessage = "Please enter the number of rows (This can be changed later)")]
        public int RowCount { get; set; }

        [Required (ErrorMessage = "Please enter the number of columns (This can be changed later)")]        
        public int ColumnCount { get; set; }
        
        [HiddenInput (DisplayValue = false)]
        public string OwnerId { get; set; }
    }
}