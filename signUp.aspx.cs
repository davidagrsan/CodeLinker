using CodeLinker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class SignUp : System.Web.UI.Page
    {
        DALUser uDAL = new DALUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            var header = Master.FindControl("navbar__header") as HtmlGenericControl;
            header.Style["display"] = "none";

            var footer = Master.FindControl("footer") as HtmlGenericControl;
            footer.Style["display"] = "none";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtBoxUser.Text != ""|| txtBoxPwd.Text != "")
            {
                if (uDAL.logInCredentials(txtBoxUser.Text, txtBoxPwd.Text).Status)
                {
                    Session["connected"] = true;
                    Response.Redirect("Default.aspx");
                }
                else lblConnected.Text = "Usuario y/o contraseña incorrecto";
            }
        }
    }
}