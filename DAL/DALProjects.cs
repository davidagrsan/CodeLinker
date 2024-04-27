using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeLinker
{
    public class DALProjects
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();

        public List<Project> LoadProjects()
        {
            var data = dc.Project.ToList();

            return data;
        }

        public List<Project> LoadFilteredProjects(List<string> filters)
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
                                           join l in dc.ProgrammingLanguage on p.MainLanguage equals l.LanguageId
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

        public int CountParticipants(int projectId)
        {
            int countParticipants = (from up in dc.UserParticipatesProject
                                     where up.ProjectFK == projectId
                                     select up.UserFK).Distinct().Count();

            return countParticipants;
        }

        //public List<Project> LoadFilters(List<string> filters, List<int> filtersId)
        //{
        //    IQueryable<Project> allProjects = dc.Project;

        //    List<Project> filteredProjects = allProjects.ToList();

        //    foreach (string filter in filters)
        //    {
        //        switch (filter)
        //        {
        //            case "filterOpen":
        //                filteredProjects = filteredProjects.Where(p => CountParticipants(p.ProjectId) < p.MaxUsers && !p.Finalized).ToList();
        //                break;
        //            case "filterClosed":
        //                filteredProjects = filteredProjects.Where(p => CountParticipants(p.ProjectId) == p.MaxUsers && !p.Finalized).ToList();
        //                break;
        //            case "filterInProgress":
        //                filteredProjects = filteredProjects.Where(p => !p.Finalized).ToList();
        //                break;
        //            case "filterEnded":
        //                filteredProjects = filteredProjects.Where(p => p.Finalized).ToList();
        //                break;
        //            default:
        //                for (int i = 0; i < filtersId.Count; i++)
        //                {
        //                    if (filters[i].StartsWith("filterLanguage_"))
        //                    {
        //                        int filterId = filtersId[i];
        //                        filteredProjects = filteredProjects.Where(p => p.Mainlanguage == filterId).ToList();
        //                    }
        //                    else if (filters[i].StartsWith("filterType_"))
        //                    {
        //                        int filterId = filtersId[i];
        //                        filteredProjects = filteredProjects.Where(p => p.ProjectTypeFK == filterId).ToList();
        //                    }
        //                    else if (filters[i].StartsWith("filterCategory_"))
        //                    {
        //                        int filterId = filtersId[i];
        //                        filteredProjects = filteredProjects.Where(p => p.ProjectCategoryFK == filterId).ToList();
        //                    }
        //                }
        //                break;
        //        }
        //    }

        //    for (int i = 0; i < filtersId.Count; i++)
        //    {
        //        if (filters[i].StartsWith("filterLanguage_"))
        //        {
        //            int filterId = filtersId[i];
        //            filteredProjects = filteredProjects.Where(p => p.Mainlanguage == filterId).ToList();
        //        }
        //        else if (filters[i].StartsWith("filterType_"))
        //        {
        //            int filterId = filtersId[i];
        //            filteredProjects = filteredProjects.Where(p => p.ProjectTypeFK == filterId).ToList();
        //        }
        //        else if (filters[i].StartsWith("filterCategory_"))
        //        {
        //            int filterId = filtersId[i];
        //            filteredProjects = filteredProjects.Where(p => p.ProjectCategoryFK == filterId).ToList();
        //        }
        //    }

        //    return filteredProjects;
        //}

        //public List<Project> LoadFilterComboBox(List<string> filters, List<int> filtersId)
        //{
        //    IEnumerable<Project> allProjects = dc.Project;

        //    //foreach (string filter in filters)
        //    for (int i = 0; i < filters.Count(); i++)
        //    {
        //        string filter = filters[i];
        //        int filterId = filtersId[i];

        //        if (filter.StartsWith("filterLanguage_"))
        //        {
        //            allProjects = (from p in allProjects
        //                           where p.Mainlanguage == filterId
        //                           select p).ToList();
        //        }

        //        if (filter.StartsWith("filterType_"))
        //        {
        //            allProjects = (from p in allProjects
        //                           where p.ProjectTypeFK == filterId
        //                           select p).ToList();
        //        }

        //        if (filter.StartsWith("filterCategory_"))
        //        {
        //            allProjects = (from p in allProjects
        //                           where p.ProjectCategoryFK == filterId
        //                           select p).ToList();
        //        }
        //    }

        //    return allProjects.ToList();
        //}
    }
}