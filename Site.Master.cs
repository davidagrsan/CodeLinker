using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool connected = (bool)Session["connected"];
            User connectedUser = (User)Session["connectedUser"];
            if (connected)
            {
                login__text.InnerHtml = connectedUser.UserName;
                login__text.HRef = "UserProfile.aspx";
            }
        }
    }
}