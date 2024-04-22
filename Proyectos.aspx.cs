using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class Proyectos : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALFilters dalFilters = new DALFilters();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadComboBoxes();
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
            comboProgrammingLanguage.Items.Add(new ListItem("", "empty"));
            comboProgrammingLanguage.SelectedValue = "empty";

            comboType.DataSource = dalFilters.LoadType();
            comboType.DataBind();
            comboType.Items.Add(new ListItem("", "empty"));
            comboType.SelectedValue = "empty";

            comboCategory.DataSource = dalFilters.LoadCategory();
            comboCategory.DataBind();
            comboCategory.Items.Add(new ListItem("", "empty"));
            comboCategory.SelectedValue = "empty";
        }
    }
}