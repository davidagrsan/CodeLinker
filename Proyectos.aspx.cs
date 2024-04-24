using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class Proyectos : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALFilters dalFilters = new DALFilters();
        DALProjects dalProjects = new DALProjects();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadComboBoxes();
                    LoadAllProjects();
                }
            }
            catch
            {
            }
        }

        private void LoadComboBoxes()
        {
            comboProgrammingLanguage.DataSource = dalFilters.LoadProgrammingLanguage();
            comboProgrammingLanguage.DataBind();
            comboProgrammingLanguage.Items.Add(new ListItem("Lenguaje", "default"));
            comboProgrammingLanguage.SelectedValue = "default";

            comboType.DataSource = dalFilters.LoadType();
            comboType.DataBind();
            comboType.Items.Add(new ListItem("Tipo", "default"));
            comboType.SelectedValue = "default";

            comboCategory.DataSource = dalFilters.LoadCategory();
            comboCategory.DataBind();
            comboCategory.Items.Add(new ListItem("Categoría", "default"));
            comboCategory.SelectedValue = "default";
        }

        private void LoadAllProjects()
        {
            List<Project> projectList = new List<Project>();
            projectList = dalProjects.LoadProjects();

            foreach (Project project in projectList)
            {
                DrawProjectHTML(project);
            }
        }

        private void DrawProjectHTML(Project project)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            int actualParticipants = dalProjects.CountParticipants(project.ProjectId);

            // Inicio del contenedor principal
            htmlBuilder.Append("<div class=\"project__container\">");

            // Primera fila
            htmlBuilder.Append("<div class=\"project__firstRow\">");
            htmlBuilder.Append($"<p class=\"project__language\">{project.ProgrammingLanguage}</p>");
            htmlBuilder.Append("<p class=\"project__lblStartDate\">Fecha comienzo: <span class=\"project__numStartDate\">" + project.StartDate + "</span></p>");
            htmlBuilder.Append($"<p class=\"project__type\">{project.ProjectType}</p>");
            htmlBuilder.Append("<p class=\"project__lblEndDate\">Fecha límite: <span class=\"project__numEndDate\">" + project.DeliveryDate + "</span></p>");
            // Iconos diferentes según la categoría
            switch(project.Category)
            {
                // Juegos
                case 1:
                    htmlBuilder.Append("<p class=\"project__category\"><i class=\"fa-solid fa-gamepad\"></i></p>");
                    break;
                // Herramientas y utilidades
                case 2:
                    htmlBuilder.Append("<p class=\"project__category\"><i class=\"fa-solid fa-screwdriver-wrench\"></i></p>");
                    break;
                // Base de datos
                case 3:
                    htmlBuilder.Append("<p class=\"project__category\"><i class=\"fa-solid fa-database\"></i></p>");
                    break;
                // Nutrición
                case 4:
                    htmlBuilder.Append("<p class=\"project__category\"><i class=\"fa-solid fa-apple-whole\"></i></p>");
                    break;
                // Salud y bienestar
                case 5:
                    htmlBuilder.Append("<p class=\"project__category\"><i class=\"fa-solid fa-stethoscope\"></i></p>");
                    break;
            }
            htmlBuilder.Append($"<p class=\"project__category\">{project.}</p>");
            htmlBuilder.Append("</div>");

            // Segunda fila
            htmlBuilder.Append("<div class=\"project__secondRow\">");
            htmlBuilder.Append($"<h2 class=\"project__title\">{project.ProjectName}</h2>");
            htmlBuilder.Append($"<p class=\"project__shortDesc\">{project.ShortDescription}</p>");
            htmlBuilder.Append("</div>");

            // Tercera fila
            htmlBuilder.Append("<div class=\"project__thirdRow\">");
            htmlBuilder.Append("<div class=\"project__participantsContainer\">");
            htmlBuilder.Append("<i class=\"fa-solid fa-users\"></i>");
            htmlBuilder.Append("<div class=\"project__users\">");
            htmlBuilder.Append($"<p class=\"project__actualParticipants\">{actualParticipants}</p>");
            htmlBuilder.Append("<span class=\"separator\">/</span>");
            htmlBuilder.Append($"<p class=\"project__maxParticipants\">{project.MaxUsers}</p>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("<button class=\"project__moreInfo\">Ver más</button>");

            // Estado (Abierto/Cerrado y En progreso/Cerrado)
            htmlBuilder.Append("<div class=\"project__state\">");
            if (project.Open)
                htmlBuilder.Append("<span class=\"project__open\">Abierto</span>");
            else
                htmlBuilder.Append("<span class=\"project__closed\">Cerrado</span>");

            if (project.Finalized)
                htmlBuilder.Append("<span class=\"project__running\">En progreso</span>");
            else
                htmlBuilder.Append("<span class=\"project__running\">Cerrado</span>");

            // Cierre de contenedores div
            htmlBuilder.Append("</div>");

            htmlBuilder.Append("</div>");

            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }
    }
    }
}