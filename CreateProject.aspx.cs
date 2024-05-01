using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class CreateProject : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALFilters dalFilters = new DALFilters();
        DALProjects dalProjects = new DALProjects();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadComboBoxes();
            }
        }

        private void LoadComboBoxes()
        {
            // Carga de ListBox para crear proyecto con valores de BBDD
            comboProgrammingLanguage.DataSource = dalFilters.LoadProgrammingLanguage();
            comboProgrammingLanguage.DataTextField = "LanguageName";
            comboProgrammingLanguage.DataValueField = "LanguageId";
            comboProgrammingLanguage.DataBind();
            comboProgrammingLanguage.Items.Insert(0,new ListItem("Sin lenguaje seleccionado", "default"));
            comboProgrammingLanguage.SelectedValue = "default";

            comboCategory.DataSource = dalFilters.LoadCategory();
            comboCategory.DataTextField = "CategoryName";
            comboCategory.DataValueField = "CategoryId";
            comboCategory.DataBind();
            comboCategory.Items.Insert(0,new ListItem("Sin categoría seleccionada", "default"));
            comboCategory.SelectedValue = "default";
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            string startDateString = startDate.Text;
            string limitDateString = limitDate.Text;
            int propietaryId = ((User)Session["connectedUser"]).UserId;

            DateTime parsedStart = DateTime.Parse(startDateString);
            DateTime parsedLimit = DateTime.Parse(limitDateString);

            Project newProject = new Project 
            {
                ProjectName = txtProjectName.Text,
                ShortDescription = txtShortDesc.Text,
                FullDescription = txtFullDesc.Text,
                StartDate = parsedStart,
                DeliveryDate = parsedLimit,
                MaxUsers = Convert.ToInt32(comboMaxUsers.SelectedItem.Value),
                Finalized = false,
                GithubURL = githubUrl.Text,
                PropietaryId = propietaryId,
                Mainlanguage = Convert.ToInt32(comboProgrammingLanguage.SelectedValue),
                ProjectTypeFK = 3,
                ProjectCategoryFK = Convert.ToInt32(comboCategory.SelectedValue),
                CommunicationMethod = urlDiscord.Text
            };

            dalProjects.InsertProject(newProject);
            Response.Redirect($"VerProyecto.aspx?id={newProject.ProjectId}");
        }
    }
}