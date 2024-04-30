using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class Perfil : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["connected"] != null)
                {
                    GenerateProfileInfo();
                }
            }

        }

        private void GenerateProfileInfo()
        {
            // Crea la variable de la sesión del usuario conectado
            User user = (User)Session["connectedUser"];
            // Se asignan los datos básicos en los campos del perfil
            nombre.Text = user.FirstName;
            apellido.Text = user.LastName;
            email.Text = user.Email;
            telefono.Text = user.PhoneNumber;
            // Conversión de fecha a string de formato necesario
            DateTime birthDate = Convert.ToDateTime(user.BirthDate);
            fecha.Text = birthDate.ToString("yyyy-MM-dd");

            linkLinkedIn.Text = user.LinkedInURL;
            linkGitHub.Text = user.GitHubURL;

            switch (user.SpecialityFK)
            {
                case 1:
                    frontend.Checked = true;
                    break;
                case 2:
                    backend.Checked = true;
                    break;
                case 3:
                    fullstack.Checked = true;
                    break;
            }
        }

        protected void BtnCloseSession_Click(object sender, EventArgs e)
        {
            Session["connectedUser"] = null;
            Session["connected"] = false;
            Response.Redirect("Default.aspx");
        }

        protected void BtnDeleteAccount_Click(object sender, EventArgs e)
        {

        }
    }
}