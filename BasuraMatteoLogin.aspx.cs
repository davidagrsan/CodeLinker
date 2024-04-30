using CodeLinker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class BasuraMatteoLogin : System.Web.UI.Page
    {
        DALUser uDAL = new DALUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            User actualUser = (User)Session["connectedUser"];
            string profilePicture = Convert.ToBase64String(actualUser.ProfilePhoto.ToArray());
            ProfilePicture.ImageUrl = "data:image/png;base64," + profilePicture;
        }

        protected void btnDisconnect_Click(object sender, EventArgs e)
        {
            Session["connectedUser"] = null;
            Session["connected"] = false;
            Response.Redirect("Default.aspx");
        }

        protected void btnChangePhoto_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/Content/img/logo-without-letters.png");
            byte[] defaultProfilePicture = System.IO.File.ReadAllBytes(filePath);
            User actualUser = (User)Session["connectedUser"];

            actualUser.ProfilePhoto = defaultProfilePicture;
            uDAL.UpdateUser(actualUser);
        }
    }
}