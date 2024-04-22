using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeLinker.DAL
{
    public class DALUser
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();

        public (bool Status, User user) logInCredentials(string username, string password)
        {
            var query = (from user in dc.User
                         where user.UserName == username
                         select user).FirstOrDefault();

            if (query == null) return (false, null);
            if (query.Password == password) return (true, query);
            else return (false, null);
        }
    }
}