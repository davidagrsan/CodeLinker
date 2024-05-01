using CodeLinker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BCrypt.Net;

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
            string emailFormat = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            //Comprueba que no se dejen campos vacíos
            if (txtBoxUser.Text == "" || txtBoxPwd.Text == "" || txtBoxConfirmPwd.Text == "" || txtBoxEmail.Text == "")
            {
                lblCreationStatus.Text = "Por favor, rellena todos los campos";
                return;
            }
            //Comprueba el formato del correo
            if(!Regex.IsMatch(txtBoxEmail.Text, emailFormat))
            {
                lblCreationStatus.Text = "Por favor, introduzca un correo válido";
                return;
            }
            //Asegura los requisitos mínimos de complejidad de la contraseña: mínimo 8 caracteres, mayúscula, minúscula, número y carácter especial
            if (!(txtBoxPwd.Text.Length > 7 && txtBoxPwd.Text.Any(char.IsUpper) && txtBoxPwd.Text.Any(char.IsLower) && txtBoxPwd.Text.Any(char.IsDigit) && txtBoxPwd.Text.Any(c => !char.IsLetterOrDigit(c))))
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/img/logo-without-letters.png");
            byte[] defaultProfilePicture = System.IO.File.ReadAllBytes(imagePath);

            //Encriptamos la contraseña usando la librería de BCrypt.Net-Next
            //La función GenerateSalt, determina las iteraciones que hacemos al proceso de hashing de la contraseña
            //Cuantas más iteraciones, más segura, pero también más recursos requiere, lo aconsejado es usar de 10 a 12 rondas
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtBoxConfirmPwd.Text, BCrypt.Net.BCrypt.GenerateSalt(12));
            User newUser = new User
            {
                UserName = txtBoxUser.Text,
                Password = hashedPassword,
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
