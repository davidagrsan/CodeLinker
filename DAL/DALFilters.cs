using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeLinker
{
    public class DALFilters
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();

        public IQueryable LoadProgrammingLanguage()
        {
            var data = from language in dc.ProgrammingLanguage
                       select language;

            return data;
        }
        public IQueryable LoadType()
        {
            var data = from type in dc.ProjectType
                       select type;

            return data;
        }

        public IQueryable LoadCategory()
        {
            var data = from category in dc.ProjectCategory
                       select category;

            return data;
        }


    }
}