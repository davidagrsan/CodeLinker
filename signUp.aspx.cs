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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            bool creationStatus = false;
            //Comprueba que no se dejen campos vacíos
            if (txtBoxUser.Text == "" || txtBoxPwd.Text == "" || txtBoxConfirmPwd.Text == "" || txtBoxEmail.Text == "")
            {
                lblCreationStatus.Text = "Por favor, rellena todos los campos";
                return;
            }
            //Asegura los requisitos mínimos de complejidad de la contraseña: mínimo 8 caracteres, mayúscula, minúscula, número y carácter especial
            if(!(txtBoxPwd.Text.Length > 7 && txtBoxPwd.Text.Any(char.IsUpper) && txtBoxPwd.Text.Any(char.IsLower) && txtBoxPwd.Text.Any(char.IsDigit) && txtBoxPwd.Text.Any(c => !char.IsLetterOrDigit(c))))
            {
                lblCreationStatus.Text = "Por favor, asegúrese que la contraseña cumpla los requisitos de longitud y complejidad (mínimo 8 carácteres, minúscula, mayúscula, número y carácter especial";
                return;
            }
            //Comprueba que las contraseñas coincidan
            if (!txtBoxPwd.Text.Equals(txtBoxConfirmPwd.Text))
            {
                lblCreationStatus.Text = "Las contraseñas no coinciden";
                return;
            }
            //Si todo lo anterior se cumple, crea el nuevo usuario y lo sube
            byte[] defaultProfilePicture = System.IO.File.ReadAllBytes("Content/img/logo-without-letters.png");
            User newUser = new User
            {
                UserName = txtBoxUser.Text,
                Password = txtBoxConfirmPwd.Text,
                Email = txtBoxEmail.Text,
                ProfilePhoto = defaultProfilePicture,
                SpecialityFK = null,
                UserTypeFK = null
            };

            creationStatus = uDAL.CreateUser(newUser);
            //En caso de que el insert de error de tipo SqlConnectio porque username y correos son únicos, indicará que el correo o el nombre de usuario están siendo usados
            if (creationStatus)
                Response.Redirect("Default.aspx");
            else
                lblCreationStatus.Text = "El usuario o correo están en uso";

        }
    }
}
