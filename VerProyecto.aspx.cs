using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class VerProyecto : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALProjects dalProjects = new DALProjects();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int projectId = Convert.ToInt32(Request.QueryString["id"]);
                LoadProjectInfo(projectId);
            }
        }

        private void LoadProjectInfo(int projectId)
        {
            // Cargamos los datos del proyecto de la base de datos
            Project project = dalProjects.LoadSingleProject(projectId);

            // Crear el contenedor principal
            HtmlGenericControl projectContainer = new HtmlGenericControl("div");
            projectContainer.Attributes["class"] = "proyecto__container";
            projectContainer.ID = "projectContainer";
            projectContainer.EnableViewState = false;

            // Genera HTML para la primera fila
            string firstRowHtml = $@"
        <section class='container__encabezado'>
            <div class='encabezado__categoria-proyecto'>
                {GetCategoryIcon(project.ProjectCategoryFK)}
                <span class='encabezado__lenguaje'>{project.ProgrammingLanguage}</span>
            </div>
            <h2 class='encabezado__titulo'>&lt;{project.ProjectName}&gt;</h2>
            <span class='encabezado__tipo'>{project.ProjectType}</span>
        </section>";

            projectContainer.InnerHtml = $"{firstRowHtml}";
            project__Container.Controls.Add(projectContainer);
        }


        private string GetCategoryIcon(int category)
        {
            switch (category)
            {
                case 1: return "<i class='fa-solid fa-gamepad'></i>";
                case 2: return "<i class='fa-solid fa-screwdriver-wrench'></i>";
                case 3: return "<i class='fa-solid fa-database'></i>";
                case 4: return "<i class='fa-solid fa-apple-whole'></i>";
                case 5: return "<i class='fa-solid fa-stethoscope'></i>";
                default: return "";
            }
        }
    }
}