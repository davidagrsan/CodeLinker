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
                            if (languageName != "Lenguaje") // Verifica si no es el filtro predeterminado
                            {
                                allProjects = (from p in allProjects
                                               join l in dc.ProgrammingLanguage on p.MainLanguage equals l.LanguageId
                                               where l.LanguageName == languageName
                                               select p).ToList();
                            }
                        }
                        if (filter.StartsWith("filterType_"))
                        {
                            string typeName = filter.Substring("filterType_".Length);
                            if (typeName != "Tipo") // Verifica si no es el filtro predeterminado
                            {
                                allProjects = (from p in allProjects
                                               join t in dc.ProjectType on p.ProjectTypeFK equals t.ProjectTypeId
                                               where t.ProjectTypeName == typeName
                                               select p).ToList();
                            }
                        }
                        if (filter.StartsWith("filterCategory_"))
                        {
                            string categoryName = filter.Substring("filterCategory_".Length);
                            if (categoryName != "Categoría") // Verifica si no es el filtro predeterminado
                            {
                                allProjects = (from p in allProjects
                                               join c in dc.ProjectCategory on p.ProjectCategoryFK equals c.CategoryId
                                               where c.CategoryName == categoryName
                                               select p).ToList();
                            }
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
    }
}