using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["connected"] = false;
            if ((bool)Session["connected"])
            {
                HyperLink login__text = (HyperLink)FindControl("loginLink");
                login__text.Text = "Hola";
            }
        }
    }
}