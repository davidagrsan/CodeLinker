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
            var data = (from project in dc.Project
                       select project).ToList();

            return data;
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