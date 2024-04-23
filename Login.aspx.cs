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
    public partial class Login : System.Web.UI.Page
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
            if (txtBoxUserLogIn.Text != "" || txtBoxPwdLogIn.Text != "")
            {
                if (uDAL.logInCredentials(txtBoxUserLogIn.Text, txtBoxPwdLogIn.Text).Status)
                {
                    Session["connected"] = true;
                    Session["connectedUser"] = uDAL.logInCredentials(txtBoxUserLogIn.Text, txtBoxPwdLogIn.Text).user;
                    Response.Redirect("Default.aspx");
                }
                else lblConnectedLogIn.Text = "Usuario y/o contraseña incorrecto";
            }
            else
                lblConnectedLogIn.Text = "Por favor, introduzca su usuario y contraseña";
        }
    }
}