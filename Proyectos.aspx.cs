using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<Project> projectList = new List<Project>();
        HtmlGenericControl projectHtml;
        int containerIndex = 1;
        List<string> actualFilters = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // En caso de no ser postback (primera recarga), carga los comboBoxes y todos los proyectos sin filtro
                if (!IsPostBack)
                {
                    LoadComboBoxes();
                    LoadAllProjects();
                }
                else
                {
                    FilterCheckBoxes();
                    FilterProgrammingLanguage();
                    FilterType();
                    FilterCategory();
                }
            }
            catch
            {
            }
        }

        private void LoadComboBoxes()
        {
            // Carga de ListBox para escoger filtros
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
            // Carga todos los proyectos cuando ya no haya filtros (para diferentes usos)
            projectList = dalProjects.LoadProjects();

            foreach (Project project in projectList)
            {
                projectHtml = DrawProjectHTML(project);
                project__projects.Controls.Add(projectHtml);
            }
        }

        private void LoadProjectsWithFilter()
        {
            // Borra los controles actuales
            project__projects.Controls.Clear();

            // Crea una lista temporal de filtros para guardar aquí los proyectos filtrados
            List<Project> filteredProjects = new List<Project>();

            // Por cada nombre de filtro añadirá los proyectos con una búsqueda en la BBDD sin dejar de lado los anteriores ya añadidos
            // porque los saca de una variable que no se limpia hasta que se cumplen unas condiciones
            dalProjects.LoadFilteredProjects(actualFilters);

            // Por cada proyecto específico dentro de los filtrados, dibujará su contenedor con todo su HTML
            foreach (Project project in filteredProjects)
            {
                projectHtml = DrawProjectHTML(project);
                project__projects.Controls.Add(projectHtml);
            }
        }

        private HtmlGenericControl DrawProjectHTML(Project project)
        {
            int actualParticipants = dalProjects.CountParticipants(project.ProjectId);

            // Genera indices nuevos para cada contenedor ya que cada control debe tener un nombre diferente
            string containerId = $"projectContainer_{containerIndex}";
            containerIndex++;

            // Crear el contenedor principal
            HtmlGenericControl projectContainer = new HtmlGenericControl("div");
            projectContainer.Attributes["class"] = "project__container";
            projectContainer.ID = containerId;
            projectContainer.Attributes.Add("runat", "server");

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
            usersContainer.Controls.Add(new LiteralControl($"<p class=\"project__actualParticipants\">{actualParticipants}</p>"));
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

            // En caso de que el proyecto haya llegado al máximo de participantes se considerará completo automáticamente
            if (dalProjects.CountParticipants(project.ProjectId) < project.MaxUsers)
                stateContainer.InnerHtml = "<span class=\"project__open\">Abierto</span>";
            else
                stateContainer.InnerHtml = "<span class=\"project__closed\">Completo</span>";

            stateContainer.InnerHtml += project.Finalized ? "<span class=\"project__running finished\">Finalizado</span>" : "<span class=\"project__running inProgress\">En progreso</span>";

            thirdRow.Controls.Add(stateContainer);

            projectContainer.Controls.Add(thirdRow);

            return projectContainer;
        }

        private void FilterCheckBoxes()
        {
            actualFilters.RemoveAll(filter => filter == "filterOpen" || filter == "filterClosed" || filter == "filterInProgress" || filter == "filterEnded");

            if (checkOpen.Checked)
            {
                if (!checkEnded.Checked)
                {
                    actualFilters.Add("filterOpen");
                }
            }

            if (checkEnded.Checked)
            {
                if (!checkOpen.Checked)
                {
                    actualFilters.Add("filterEnded");
                }
            }

            LoadProjectsFiltered();
        }

        private void FilterProgrammingLanguage()
        {
            actualFilters.RemoveAll(filter => filter == ("filterLanguage"));

            if (comboProgrammingLanguage.SelectedItem != null)
            {
                actualFilters.Add($"filterLanguage_{comboProgrammingLanguage.SelectedItem.ToString()}");
            }

            LoadProjectsFiltered();
        }

        private void FilterType()
        {
            actualFilters.RemoveAll(filter => filter == ("filterType"));

            if (comboType.SelectedItem != null)
            {
                actualFilters.Add($"filterType_{comboType.SelectedItem.ToString()}");
            }

            LoadProjectsFiltered();
        }

        private void FilterCategory()
        {
            actualFilters.RemoveAll(filter => filter == ("filterCategory"));

            if (comboCategory.SelectedItem != null)
            {
                actualFilters.Add($"filterCategory_{comboCategory.SelectedItem.ToString()}");
            }

            LoadProjectsFiltered();
        }

        private void LoadProjectsFiltered()
        {
            // Borra los controles actuales
            project__projects.Controls.Clear();

            // Crea una lista temporal de filtros para guardar aquí los proyectos filtrados
            List<Project> filteredProjects = new List<Project>();

            filteredProjects = dalProjects.LoadFilteredProjects(actualFilters);

            // Por cada proyecto específico dentro de los filtrados, dibujará su contenedor con todo su HTML
            foreach (Project project in filteredProjects)
            {
                projectHtml = DrawProjectHTML(project);
                project__projects.Controls.Add(projectHtml);
            }
        }

        protected void butClean_Click(object sender, EventArgs e)
        {
            CleanAllFilters();
        }        

        private void CleanAllFilters()
        {
            project__projects.Controls.Clear();
            LoadAllProjects();
        }

    }
}