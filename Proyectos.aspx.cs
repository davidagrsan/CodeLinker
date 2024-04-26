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
        List<string> filterNames = new List<string>();

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
                    // Si ha recargado en postback significa que hay un filtro activándose, por lo que se comprueban todos los posibles
                    CheckOpen();
                    CheckClosed();
                    CheckInProgress();
                    CheckEnded();
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

        private void LoadProjectsFiltered()
        {
            // Borra los controles actuales
            project__projects.Controls.Clear();

            // Crea una lista temporal de filtros para guardar aquí los proyectos filtrados
            List<Project> filteredProjects = new List<Project>();

            // Por cada nombre de filtro añadirá los proyectos con una búsqueda en la BBDD sin dejar de lado los anteriores ya añadidos
            // porque los saca de una variable que no se limpia hasta que se cumplen unas condiciones
            foreach (string filterName in filterNames)
            {
                switch (filterName)
                {
                    case "filterOpen":
                        filteredProjects.AddRange(dalProjects.LoadOpenClosedProjects(true));
                        break;
                    case "filterClosed":
                        filteredProjects.AddRange(dalProjects.LoadOpenClosedProjects(false));
                        break;
                    case "filterInProgress":
                        filteredProjects.AddRange(dalProjects.LoadInProgressEndedProjects(true));
                        break;
                    case "filterEnded":
                        filteredProjects.AddRange(dalProjects.LoadInProgressEndedProjects(false));
                        break;
                    default:
                        CleanFilters();
                        break;
                }
            }

            // Por cada proyecto específico dentro de los filtrados, dibujará su contenedor con todo su HTML
            foreach (Project project in filteredProjects)
            {
                projectHtml = DrawProjectHTML(project);
                project__projects.Controls.Add(projectHtml);
            }
        }

        private HtmlGenericControl DrawProjectHTML(Project project)
        {
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
            stateContainer.InnerHtml = project.Open ? "<span class=\"project__open\">Abierto</span>" : "<span class=\"project__closed\">Completo</span>";
            stateContainer.InnerHtml += project.Finalized ? "<span class=\"project__running inProgress\">En progreso</span>" : "<span class=\"project__running finished\">Finalizado</span>";
            thirdRow.Controls.Add(stateContainer);

            projectContainer.Controls.Add(thirdRow);

            return projectContainer;
        }

        private void CheckOpen()
        {
            if (checkOpen.Checked)
            {
                filterNames.Add("filterOpen");
                LoadProjectsFiltered();
            }
            else if (!checkOpen.Checked)
            {
                filterNames.Remove("filterOpen");
                LoadProjectsFiltered();
            }
        }

        private void CheckClosed()
        {
            if (checkClosed.Checked)
            {
                filterNames.Add("filterClosed");
                LoadProjectsFiltered();
            }
            else if (!checkClosed.Checked)
            {
                filterNames.Remove("filterClosed");
                LoadProjectsFiltered();
            }
        }

        private void CheckInProgress()
        {
            if (checkInProgress.Checked)
            {
                filterNames.Add("filterInProgress");
                LoadProjectsFiltered();
            }
            else if (!checkInProgress.Checked)
            {
                filterNames.Remove("filterInProgress");
                LoadProjectsFiltered();
            }
        }

        private void CheckEnded()
        {
            if (checkEnded.Checked)
            {
                filterNames.Add("filterEnded");
                LoadProjectsFiltered();
            }
            else if (!checkEnded.Checked)
            {
                filterNames.Remove("filterEnded");
                LoadProjectsFiltered();
            }
        }

        protected void butClean_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }        

        private void CleanFilters()
        {
            project__projects.Controls.Clear();
            LoadAllProjects();
        }
    }
}