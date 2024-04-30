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
    public partial class Projects : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALFilters dalFilters = new DALFilters();
        DALProjects dalProjects = new DALProjects();
        List<Project> projectList = new List<Project>();
        HtmlGenericControl projectHtml;
        int containerIndex = 1;
        List<string> actualFilters = new List<string>();
        List<int> actualFiltersId = new List<int>();

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
                    actualFiltersId.Clear();
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
            comboProgrammingLanguage.DataTextField = "LanguageName";
            comboProgrammingLanguage.DataValueField = "LanguageId";
            comboProgrammingLanguage.DataBind();
            comboProgrammingLanguage.Items.Insert(0, new ListItem("Sin lenguaje seleccionado", "default"));
            comboProgrammingLanguage.SelectedValue = "default";
            comboProgrammingLanguage.SelectedIndex = 0;

            comboType.DataSource = dalFilters.LoadType();
            comboType.DataTextField = "ProjectTypeName";
            comboType.DataValueField = "ProjectTypeId";
            comboType.DataBind();
            comboType.Items.Insert(0, new ListItem("Sin tipo seleccionado", "default"));
            comboType.SelectedValue = "default";
            comboType.SelectedIndex = 0;

            comboCategory.DataSource = dalFilters.LoadCategory();
            comboCategory.DataTextField = "CategoryName";
            comboCategory.DataValueField = "CategoryId";
            comboCategory.DataBind();
            comboCategory.Items.Insert(0, new ListItem("Sin categoría seleccionada", "default"));
            comboCategory.SelectedValue = "default";
            comboCategory.SelectedIndex = 0;
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

        // Función general para dibujar el HTMl de los proyectos
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

            // Genera HTML para la primera fila
            string firstRowHtml = $@"
        <div class='project__firstRow'>
            <p class='project__language'>{project.ProgrammingLanguage}</p>
            <p class='project__lblStartDate'>Fecha comienzo: <span class='project__numStartDate'>{project.StartDate.ToString("dd/MM/yyyy")}</span></p>
            <p class='project__category'>{GetCategoryIcon(project.ProjectCategoryFK)}</p>
            <p class='project__lblEndDate'>Fecha límite: <span class='project__numEndDate'>{project.DeliveryDate.ToString("dd/MM/yyyy")}</span></p>
            <p class='project__type'>{project.ProjectType}</p>
        </div>";

            // Genera HTML para la segunda fila
            string secondRowHtml = $@"
        <div class='project__secondRow'>
            <h2 class='project__title'>{project.ProjectName}</h2>
            <p class='project__shortDesc'>{project.ShortDescription}</p>
        </div>";

            // Genera HTML para la tercera fila
            string thirdRowHtml = $@"
        <div class='project__thirdRow'>
            <div class='project__participantsContainer'>
                <i class='fa-solid fa-users'></i>
                <div class='project__users'>
                    <p class='project__actualParticipants'>{actualParticipants}</p>
                    <span class='separator'>/</span>
                    <p class='project__maxParticipants'>{project.MaxUsers}</p>
                </div>
            </div>
            <a href='VerProyecto.aspx?id={project.ProjectId}' class='project__moreInfo'>Ver más</a>
            <div class='project__state'>{GetProjectStateHtml(project)}</div>
        </div>";

            // Asigna HTML generado a los contenedores
            projectContainer.InnerHtml = $"{firstRowHtml}{secondRowHtml}{thirdRowHtml}";

            return projectContainer;
        }

        // Función para obtener el icono de categoría
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

        // Función para obtener el estado del proyecto
        private string GetProjectStateHtml(Project project)
        {
            int participantsCount = dalProjects.CountParticipants(project.ProjectId);
            string stateHtml = participantsCount < project.MaxUsers ?
                "<span class='project__open'>Abierto</span>" :
                "<span class='project__closed'>Completo</span>";

            stateHtml += project.Finalized ? "<span class='project__running finished'>Finalizado</span>" :
                "<span class='project__running inProgress'>En progreso</span>";

            return stateHtml;
        }

        // Filtro de las checkboxes
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

            //List<Project> checkBoxProjects = dalProjects.LoadFilters(actualFilters, actualFiltersId);
            List<Project> checkBoxProjects = dalProjects.LoadFilteredProjects(actualFilters);

            LoadProjectsFiltered(checkBoxProjects);
        }

        // Filtro del combobox de lenguajes de programación
        private void FilterProgrammingLanguage()
        {
            actualFilters.RemoveAll(filter => filter == ("filterLanguage"));

            if (comboProgrammingLanguage.SelectedValue != "default")
            {
                actualFilters.Add($"filterLanguage_{comboProgrammingLanguage.SelectedItem}");

                int languageId = Convert.ToInt32(comboProgrammingLanguage.SelectedValue.ToString());

                actualFiltersId.Add(languageId);

                //List<Project> languageProjects = dalProjects.LoadFilters(actualFilters, actualFiltersId);
                List<Project> languageProjects = dalProjects.LoadFilteredProjects(actualFilters);

                LoadProjectsFiltered(languageProjects);
            }
        }

        // Filtro del combobox de tipo
        private void FilterType()
        {
            actualFilters.RemoveAll(filter => filter == ("filterType"));

            if (comboType.SelectedValue != "default")
            {
                actualFilters.Add($"filterType_{comboType.SelectedItem}");

                int typeId = Convert.ToInt32(comboCategory.SelectedValue.ToString());

                actualFiltersId.Add(typeId);

                //List<Project> typeProjects = dalProjects.LoadFilters(actualFilters, actualFiltersId);
                List<Project> typeProjects = dalProjects.LoadFilteredProjects(actualFilters);

                LoadProjectsFiltered(typeProjects);
            }
        }

        // Filtro del combobox de categoría
        private void FilterCategory()
        {
            actualFilters.RemoveAll(filter => filter == ("filterCategory"));

            if (comboCategory.SelectedValue != "default")
            {
                actualFilters.Add($"filterCategory_{comboCategory.SelectedItem}");

                int categoryId = Convert.ToInt32(comboCategory.SelectedValue.ToString());

                actualFiltersId.Add(categoryId);

                //List<Project> categoryProjects = dalProjects.LoadFilters(actualFilters, actualFiltersId);
                List<Project> categoryProjects = dalProjects.LoadFilteredProjects(actualFilters);

                LoadProjectsFiltered(categoryProjects);
            }
        }

        private void LoadProjectsFiltered(List<Project> projects)
        {
            // Borra los controles actuales
            project__projects.Controls.Clear();

            //// Crea una lista temporal de filtros para guardar aquí los proyectos filtrados
            //List<Project> filteredProjects = new List<Project>();

            List<Project> filteredProjects = projects;

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
            checkOpen.Checked = false;
            checkEnded.Checked = false;
            comboProgrammingLanguage.SelectedIndex = 0;
            comboType.SelectedIndex = 0;
            comboCategory.SelectedIndex = 0;

            LoadAllProjects();
        }
    }
}