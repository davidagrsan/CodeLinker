using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
                HtmlGenericControl projectHtml = DrawProjectHTML(project);
                project__projects.Controls.Add(projectHtml);
            }
        }

        private HtmlGenericControl DrawProjectHTML(Project project)
        {
            // Crear el contenedor principal
            HtmlGenericControl projectContainer = new HtmlGenericControl("div");
            projectContainer.Attributes["class"] = "project__container";

            // Primera fila
            HtmlGenericControl firstRow = new HtmlGenericControl("div");
            firstRow.Attributes["class"] = "project__firstRow";
            firstRow.Controls.Add(new LiteralControl($"<p class=\"project__language\">{project.ProgrammingLanguage}</p>"));
            firstRow.Controls.Add(new LiteralControl($"<p class=\"project__lblStartDate\">Fecha comienzo: <span class=\"project__numStartDate\">{project.StartDate.ToString("dd/MM/yyyy")}</span></p>"));

            // Icono de categoría
            HtmlGenericControl categoryIcon = new HtmlGenericControl("p");
            categoryIcon.Attributes["class"] = "project__category";
            switch (project.ProjectCategoryFK)
            {
                case 1:
                    categoryIcon.InnerHtml = "<i class=\"fa-solid fa-gamepad\"></i>";
                    break;
                case 2:
                    categoryIcon.InnerHtml = "<i class=\"fa-solid fa-screwdriver-wrench\"></i>";
                    break;
                case 3:
                    categoryIcon.InnerHtml = "<i class=\"fa-solid fa-database\"></i>";
                    break;
                case 4:
                    categoryIcon.InnerHtml = "<i class=\"fa-solid fa-apple-whole\"></i>";
                    break;
                case 5:
                    categoryIcon.InnerHtml = "<i class=\"fa-solid fa-stethoscope\"></i>";
                    break;
            }
            firstRow.Controls.Add(categoryIcon);

            firstRow.Controls.Add(new LiteralControl($"<p class=\"project__lblEndDate\">Fecha límite: <span class=\"project__numEndDate\">{project.DeliveryDate.ToString("dd/MM/yyyy")}</span></p>"));

            firstRow.Controls.Add(new LiteralControl($"<p class=\"project__type\">{project.ProjectType}</p>"));

            projectContainer.Controls.Add(firstRow);

            // Segunda fila
            HtmlGenericControl secondRow = new HtmlGenericControl("div");
            secondRow.Attributes["class"] = "project__secondRow";
            secondRow.Controls.Add(new LiteralControl($"<h2 class=\"project__title\">{project.ProjectName}</h2>"));
            secondRow.Controls.Add(new LiteralControl($"<p class=\"project__shortDesc\">{project.ShortDescription}</p>"));
            projectContainer.Controls.Add(secondRow);

            // Tercera fila
            HtmlGenericControl thirdRow = new HtmlGenericControl("div");
            thirdRow.Attributes["class"] = "project__thirdRow";

            // Participantes
            HtmlGenericControl participantsContainer = new HtmlGenericControl("div");
            participantsContainer.Attributes["class"] = "project__participantsContainer";
            participantsContainer.InnerHtml = "<i class=\"fa-solid fa-users\"></i>";

            HtmlGenericControl usersContainer = new HtmlGenericControl("div");
            usersContainer.Attributes["class"] = "project__users";
            usersContainer.Controls.Add(new LiteralControl($"<p class=\"project__actualParticipants\">{dalProjects.CountParticipants(project.ProjectId)}</p>"));
            usersContainer.Controls.Add(new LiteralControl("<span class=\"separator\">/</span>"));
            usersContainer.Controls.Add(new LiteralControl($"<p class=\"project__maxParticipants\">{project.MaxUsers}</p>"));
            participantsContainer.Controls.Add(usersContainer);

            thirdRow.Controls.Add(participantsContainer);

            // Botón "Ver más"
            HtmlGenericControl moreInfoButton = new HtmlGenericControl("button");
            moreInfoButton.Attributes["class"] = "project__moreInfo";
            moreInfoButton.InnerText = "Ver más";
            thirdRow.Controls.Add(moreInfoButton);

            // Estado del proyecto
            HtmlGenericControl stateContainer = new HtmlGenericControl("div");
            stateContainer.Attributes["class"] = "project__state";
            stateContainer.InnerHtml = project.Open ? "<span class=\"project__open\">Abierto</span>" : "<span class=\"project__closed\">Cerrado</span>";
            stateContainer.InnerHtml += project.Finalized ? "<span class=\"project__running\">En progreso</span>" : "<span class=\"project__running\">Cerrado</span>";
            thirdRow.Controls.Add(stateContainer);

            projectContainer.Controls.Add(thirdRow);

            return projectContainer;
        }
    }
}