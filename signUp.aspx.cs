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
            //Check if all textboxes have any kind of Value
            if (txtBoxUser.Text == "" || txtBoxPwd.Text == "" || txtBoxConfirmPwd.Text == "" || txtBoxEmail.Text == "")
            {
                lblCreationStatus.Text = "Por favor, rellena todos los campos";
                return;
            }
            //Checks for password requirements: Length >= 8, UpperCase, LowerCase, Number and special Character
            if(!(txtBoxPwd.Text.Length > 7 && txtBoxPwd.Text.Any(char.IsUpper) && txtBoxPwd.Text.Any(char.IsLower) && txtBoxPwd.Text.Any(char.IsDigit) && txtBoxPwd.Text.Any(c => !char.IsLetterOrDigit(c))))
            {
                lblCreationStatus.Text = "Por favor, asegúrese que la contraseña cumpla los requisitos de longitud y complejidad (mínimo 8 carácteres, minúscula, mayúscula, número y carácter especial";
                return;
            }
            //Checks if both passwords matches
            if (!txtBoxPwd.Text.Equals(txtBoxConfirmPwd.Text))
            {
                lblCreationStatus.Text = "Las contraseñas no coinciden";
                return;
            }
            //If all the checks are passed, it will create the new user
            User newUser = new User
            {
                UserName = txtBoxUser.Text,
                Password = txtBoxConfirmPwd.Text,
                Email = txtBoxEmail.Text,
                SpecialityFK = null,
                UserTypeFK = null
            };

            creationStatus = uDAL.CreateUser(nuevoUsuario);
            //If the Insert gets an exception which type is SqlConnection (Since UserName and Email are Unique constraints) it will tell the user that email or user are on use
            if (creationStatus)
                Response.Redirect("Default.aspx");
            else
                lblCreationStatus.Text = "El usuario o correo están en uso";

        }
    }
}
