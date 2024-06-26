﻿using CodeLinker.DAL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;
using System.IO;

namespace CodeLinker
{
    public partial class Perfil : System.Web.UI.Page
    {
        // Variables generales
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALUser dalUser = new DALUser();
        User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Al cargar la página se le da a la variable "user" la sesión del usuario conectado
            user = (User)Session["connectedUser"];
            if (user != null)
            {
                if (!IsPostBack)
                {
                    if (Session["connected"] != null)
                    {
                        divswitch.Attributes.Add("onclick", "toggleSwitch();");
                        GenerateProfileInfo();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        // Genera la información del perfil del usuario
        private void GenerateProfileInfo()
        {
            User actualUser = (User)Session["connectedUser"];
            string profilePicture = Convert.ToBase64String(actualUser.ProfilePhoto.ToArray());

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

        // Método para cerrar la sesión
        protected void BtnCloseSession_Click(object sender, EventArgs e)
        {
            Session["connectedUser"] = null;
            Session["connected"] = false;
            Response.Redirect("Default.aspx");
        }

        // Método para borrar cuenta
        protected void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            user = (User)Session["connectedUser"];

            string respuesta = confirmValue.Value;
            if (respuesta == "Si")
            {
                dalUser.DeleteUser(user);
                Response.Redirect("Default.aspx");
            }
        }

        // Método para guardar los cambios de contraseña
        protected void savePassChanges__btn_Click(object sender, EventArgs e)
        {
            user = (User)Session["connectedUser"];

            string storedPassword = user.Password;
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(txtActualPass.Text, storedPassword);

            if (passwordMatches)
            {
                if (txtNewPass.Text != txtRepNewPass.Text)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('¡La nueva contraseña no coincide!')", true);
                }
                // Comprobación de que los campos no están vacíos
                else if (txtNewPass.Text == "" || txtRepNewPass.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('¡La nueva contraseña no pueden ser campos vacíos!')", true);
                }
                else
                {

                    user.Password = txtNewPass.Text;
                    dalUser.UpdatePassword(user, user.Password);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('¡Tu contraseña se cambió correctamente!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('La contraseña actual no coincide...')", true);
                return;
            }
        }

        protected void BtnSaveProfileChanges_Click(object sender, EventArgs e)
        {
            user = (User)Session["connectedUser"];

            string fileName = Path.GetFileName(photoFile.FileName);
            int fileSize = photoFile.PostedFile.ContentLength;

            byte[] changedProfilePicture = new byte[fileSize];
            photoFile.PostedFile.InputStream.Read(changedProfilePicture, 0, fileSize);

            // Datos básicos, desde el campo de texto al usuario de la sesión
            user.UserName = username.Text;
            user.FirstName = nombre.Text;
            user.LastName = apellido.Text;
            user.Email = email.Text;
            user.PhoneNumber = telefono.Text;
            user.BirthDate = Convert.ToDateTime(fecha.Text);
            user.LinkedInURL = linkLinkedIn.Text;
            user.GitHubURL = linkLinkedIn.Text;

            user.ProfilePhoto = changedProfilePicture;

            // Especialidad del usuario
            if (frontend.Checked)
            {
                user.SpecialityFK = 1;
            }
            else if (backend.Checked)
            {
                user.SpecialityFK = 2;
            }
            else
            {
                user.SpecialityFK = 3;
            }

            // Privacidad del usuario
            if (divswitchoff.Attributes["class"].Contains("switch__active"))
            {
                string idPrivacidadActiva = divswitchoff.ID;

                if (divswitchoff.Attributes["class"] == "switch__active")
                    user.Privacy = false;
                if (divswitchon.Attributes["class"] == "switch__active")
                    user.Privacy = true;
                else
                    user.Privacy = false;
            }

            dalUser.UpdateUser(user);
            Session["connectedUser"] = user;
            Response.Redirect("Perfil.aspx");
        }
    }
}