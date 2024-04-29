using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodeLinker.DAL
{
    public class DALUser
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();

        public (bool Status, User user) logInCredentials(string userOrMail, string password)
        {
            try
            {
                var query = (from user in dc.User
                             where user.UserName == userOrMail || user.Email == userOrMail
                             select user).FirstOrDefault();

                if (query == null) 
                    return (false, null);
                if (query.Password == password) 
                    return (true, query);
                else 
                    return (false, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateUser(User createdUser)
        {
            try
            {
                dc.User.InsertOnSubmit(createdUser);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                if(ex is SqlException)
                {
                    return false;
                }
            }
            return false;
        }

        public void UpdateUser(User updatedUser)
        {
            try
            {
                var query = (from user in dc.User
                             where user.UserId == updatedUser.UserId
                             select user).FirstOrDefault();

                query.ProfilePhoto = updatedUser.ProfilePhoto;
                dc.SubmitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}