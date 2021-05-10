using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectAutoImplementedAuthentication.Abstract;
using FinalProjectAutoImplementedAuthentication.Models;

namespace FinalProjectAutoImplementedAuthentication.Concrete
{
    public class EFUserListRepository : IUserListRespository
    {
        private EFDbUserListContext context = new EFDbUserListContext();
        public IEnumerable<UserList> UserLists  //Creating an enumerated list that the data from the database will be stored in. This is done so that we can modify the data since we cannot directly modify the data contained within a DbContext list.
        {
            get { return context.UserLists; }
        }

        public void SaveUserList(UserList userlist)
        {
            if (userlist.ListId == 0) //If the list does not already exist
            {
                context.UserLists.Add(userlist);
            }
            
            else  //If the list is being modified
            {
                UserList dbEntry = context.UserLists.Find(userlist.ListId);

                if (dbEntry != null)  //Storing the changes that have been made
                {
                    dbEntry.ListId = userlist.ListId;
                    dbEntry.ListName = userlist.ListName;
                    dbEntry.RowCount = userlist.RowCount;
                    dbEntry.ColumnCount = userlist.ColumnCount;
                    dbEntry.OwnerId = userlist.OwnerId;
                }
            }
            context.SaveChanges();
        }

        public UserList DeleteUserList(int listId) //Takes in the ListId as a parameter to locate the list that is to be deleted.
        {
            UserList dbEntry = context.UserLists.Find(listId);
            if (dbEntry != null) // Making sure that the list actually exists
            {
                context.UserLists.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}