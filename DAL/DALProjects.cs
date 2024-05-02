using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodeLinker
{
    public class DALProjects
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();

        public List<Project> LoadProjects()
        {
            try
            {
                var data = dc.Project.ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Project LoadSingleProject(int projectId)
        {
            try
            {
                var project = dc.Project.FirstOrDefault(p => p.ProjectId == projectId);

                return project;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Project> LoadFilteredProjects(List<string> filters)
        {
            try
            {
                // Obtener todos los proyectos de la base de datos
                IEnumerable<Project> allProjects = dc.Project;

                foreach (string filter in filters)
                {
                    switch (filter)
                    {
                        case "filterOpen":
                            allProjects = allProjects.Where(p => CountParticipants(p.ProjectId) < p.MaxUsers && !p.Finalized);
                            break;
                        case "filterClosed":
                            allProjects = allProjects.Where(p => CountParticipants(p.ProjectId) == p.MaxUsers && !p.Finalized);
                            break;
                        case "filterInProgress":
                            allProjects = allProjects.Where(p => p.Finalized == false);
                            break;
                        case "filterEnded":
                            allProjects = allProjects.Where(p => p.Finalized == true);
                            break;
                        default:
                            if (filter.StartsWith("filterLanguage_"))
                            {
                                string languageName = filter.Substring("filterLanguage_".Length);
                                allProjects = (from p in allProjects
                                               join l in dc.ProgrammingLanguage on p.Mainlanguage equals l.LanguageId
                                               where l.LanguageName == languageName
                                               select p).ToList();
                            }
                            if (filter.StartsWith("filterType_"))
                            {
                                string typeName = filter.Substring("filterType_".Length);
                                allProjects = (from p in allProjects
                                               join t in dc.ProjectType on p.ProjectTypeFK equals t.ProjectTypeId
                                               where t.ProjectTypeName == typeName
                                               select p).ToList();
                            }
                            if (filter.StartsWith("filterCategory_"))
                            {
                                string categoryName = filter.Substring("filterCategory_".Length);
                                allProjects = (from p in allProjects
                                               join c in dc.ProjectCategory on p.ProjectCategoryFK equals c.CategoryId
                                               where c.CategoryName == categoryName
                                               select p).ToList();
                            }
                            break;
                    }
                }

                return allProjects.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertNewUserIntoProject(int projectId, int userId)
        {
            try
            {
                UserParticipatesProject newUserProject = new UserParticipatesProject();
                newUserProject.UserFK = userId;
                newUserProject.ProjectFK = projectId;

                dc.UserParticipatesProject.InsertOnSubmit(newUserProject);

                dc.SubmitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUserFromProject(int projectId, int userId)
        {
            try
            {
                var userProject = (from up in dc.UserParticipatesProject
                                   where up.UserFK == userId && up.ProjectFK == projectId
                                   select up).FirstOrDefault();

                if (userProject != null)
                {
                    dc.UserParticipatesProject.DeleteOnSubmit(userProject);
                    dc.SubmitChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckUserInProject(int projectId, int userId)
        {
            var userExists = from userProject in dc.UserParticipatesProject
                             where userProject.UserFK == userId && userProject.ProjectFK == projectId
                             select userProject;

            return userExists.Any();
        }

        public int CountParticipants(int projectId)
        {
            try
            {
                int countParticipants = (from up in dc.UserParticipatesProject
                                         where up.ProjectFK == projectId
                                         select up.UserFK).Distinct().Count();

                return countParticipants;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<User> ListUsersInProject(int projectId)
        {
            try
            {
                var usersInsideProject = (from u in dc.UserParticipatesProject
                                          join user in dc.User on u.UserFK equals user.UserId
                                          where u.ProjectFK == projectId
                                          select u.User).ToList();

                return usersInsideProject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertProject(Project newProject)
        {
            try
            {
                dc.Project.InsertOnSubmit(newProject);
                dc.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}