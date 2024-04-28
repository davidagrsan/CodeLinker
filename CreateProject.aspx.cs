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
            comboProgrammingLanguage.Items.Add(new ListItem("Sin lenguaje seleccionado", "default"));
            comboProgrammingLanguage.SelectedValue = "default";

            comboCategory.DataSource = dalFilters.LoadCategory();
            comboCategory.DataTextField = "CategoryName";
            comboCategory.DataValueField = "CategoryId";
            comboCategory.DataBind();
            comboCategory.Items.Add(new ListItem("Sin categoría seleccionada", "default"));
            comboCategory.SelectedValue = "default";
        }
    }
}