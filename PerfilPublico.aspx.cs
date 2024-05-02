using CodeLinker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class PerfilPublico : System.Web.UI.Page
    {
        DALUser dalUser = new DALUser();
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Comprobamos que tenga id antes de iniciar la página, si no lo es redirige a Default
                if (int.TryParse(Request.QueryString["id"], out userId))
                {
                    User user = dalUser.SearchUserWithId(userId);

                    GenerateProfileInfo(user);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void GenerateProfileInfo(User user)
        {
            string profilePicture = Convert.ToBase64String(user.ProfilePhoto.ToArray());

            // Se asignan los datos básicos en los campos del perfil
            username.Text = user.UserName;
            nombre.Text = user.FirstName;
            apellido.Text = user.LastName;
            email.Text = user.Email;
            telefono.Text = user.PhoneNumber;

            //Imagen
            fotoPerfil.Src = "data:image/png;base64," + profilePicture;

            // Conversión de fecha a string de formato necesario
            DateTime birthDate = Convert.ToDateTime(user.BirthDate);
            fecha.Text = birthDate.ToString("yyyy-MM-dd");

            // Links a LinkedIn y Github
            linkLinkedIn.NavigateUrl = user.LinkedInURL;
            linkGitHub.NavigateUrl = user.GitHubURL;

            // Opción de checkbox (está desactivada por defecto al ser perfil público)
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
    }
}