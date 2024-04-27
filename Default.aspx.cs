using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nombre de la página
            Page.Title = ConfigurationManager.AppSettings["Inicio"];
            // Deshabilitamos el footer en la página Home
            var footer = Master.FindControl("footer") as HtmlGenericControl;
            footer.Style["display"] = "none";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}