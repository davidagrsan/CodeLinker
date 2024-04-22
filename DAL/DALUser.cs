using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeLinker.DAL
{
    public class DALUser
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();

        public bool logInCredentials(string username, string password)
        {
            var query = (from user in dc.User
                         where user.UserName == username
                         select user).FirstOrDefault();

            if (query == null) return false;
            if (query.Password == password) return true;
            else return false;
        }
    }
}