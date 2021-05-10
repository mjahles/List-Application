using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectAutoImplementedAuthentication.Models;

namespace FinalProjectAutoImplementedAuthentication.Abstract
{
    public interface IUserListRespository
    {
        IEnumerable<UserList> UserLists { get; }

        void SaveUserList(UserList userList);

        UserList DeleteUserList(int ListId);
    }
}
