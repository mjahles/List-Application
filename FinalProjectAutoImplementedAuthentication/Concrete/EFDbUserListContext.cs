using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectAutoImplementedAuthentication.Models;
using System.Data.Entity;

namespace FinalProjectAutoImplementedAuthentication.Concrete
{
    public class EFDbUserListContext : DbContext
    {
        public DbSet<UserList> UserLists { get; set; }
    }
}