using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BCrypt.Net;

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

                //Es necesario recoger la contraseña enviada por la base de datos, guardarla en una string y verificarla por separado
                string storedPassword = query.Password;
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, storedPassword);

                if (passwordMatches)
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
                query.UserName = updatedUser.UserName;
                query.FirstName = updatedUser.FirstName;
                query.LastName = updatedUser.LastName;
                query.Email = updatedUser.Email;
                query.Password = updatedUser.Password;
                query.PhoneNumber = updatedUser.PhoneNumber;
                query.BirthDate = updatedUser.BirthDate;
                query.LinkedInURL = updatedUser.LinkedInURL;
                query.GitHubURL = updatedUser.GitHubURL;
                query.SpecialityFK = updatedUser.SpecialityFK;
                query.Privacy = updatedUser.Privacy;
                dc.SubmitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(User userToDelete)
        {
            try
            {
                var query = (from user in dc.User
                             where user.UserId == userToDelete.UserId
                             select user).FirstOrDefault();

                dc.User.DeleteOnSubmit(query);
                dc.SubmitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePassword(User updatedUser, string password)
        {
            try
            {
                var query = (from user in dc.User
                             where user.UserId == updatedUser.UserId
                             select user).FirstOrDefault();

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));

                query.Password = hashedPassword;

                dc.SubmitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckPassword(User connectedUser, string password)
        {
            try
            {
                var query = (from user in dc.User
                             where user.UserId == connectedUser.UserId
                             select user).FirstOrDefault();

                return query.Password == password;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}