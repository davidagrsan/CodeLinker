using CodeLinker.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CodeLinker
{
    public partial class VerProyecto : System.Web.UI.Page
    {
        DBConnectionDataContext dc = new DBConnectionDataContext();
        DALProjects dalProjects = new DALProjects();
        int projectId;
        int userId;
        User user;
        protected Button btnApuntarse;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                projectId = Convert.ToInt32(Request.QueryString["id"]);
                user = (User)Session["connectedUser"];
                if (user != null)
                {
                    userId = user.UserId;
                    bool userIsConnected = true;
                    LoadProjectInfo(projectId, userIsConnected);
                }
                else
                {
                    bool userIsConnected = false;
                    LoadProjectInfo(projectId, userIsConnected);
                }
            }
        }

        private void LoadProjectInfo(int projectId, bool userIsConnected)
        {
            // Cargamos los datos del proyecto de la base de datos
            Project project = dalProjects.LoadSingleProject(projectId);

            // Contar participantes actuales
            int actualParticipants = dalProjects.CountParticipants(project.ProjectId);

            // Estado del proyecto según los participantes
            string estadoProyecto = project.MaxUsers > actualParticipants ? "abierto" : "completo";

            // Creamos la variable para comprobar si el usuario está en el proyecto ya
            bool userIsInProject = dalProjects.CheckUserInProject(projectId, userId);

            // Crear el contenedor principal
            HtmlGenericControl projectContainer = new HtmlGenericControl("div");
            projectContainer.Attributes["class"] = "proyecto__container";
            projectContainer.ID = "projectContainer";
            projectContainer.EnableViewState = false;

            // Genera HTML para la primera fila
            string firstRowHtml = $@"
        <section class='container__encabezado'>
            <div class='encabezado__categoria-proyecto'>
                {GetCategoryIcon(project.ProjectCategoryFK)}
                <span class='encabezado__lenguaje'>{project.ProgrammingLanguage}</span>
            </div>
            <h2 class='encabezado__titulo'>&lt;{project.ProjectName}&gt;</h2>
            <span class='encabezado__tipo'>{project.ProjectType}</span>
        </section>";

            string secondRowHtml = $@"
        <section class='container__descripcion'>
            <p class='descripcion__texto'>{project.FullDescription}</p>
        </section>";

            string thirdRowHtml = $@"
        <section class='container__info'>
            <div class='info__datos'>
                <div class='datos__fechas'>
                    <div class='fechas__comienzo'>
                        <p>Fecha de inicio</p>
                        <span>{project.StartDate.ToString("dd/MM/yyyy")}</span>
                    </div>
                    <span>—</span>
                    <div class='fechas__limite'>
                        <p>Fecha límite</p>
                        <span>{project.DeliveryDate.ToString("dd/MM/yyyy")}</span>
                    </div>
                </div>

                <div class='datos__miembros'>
                    <p>Actualmente hay</p>
                    <div class='miembros__cantidad'>
                        <i class='fa-solid fa-users'></i>
                        <div class='cantidad__num-miembros'>
                            <span>{actualParticipants}</span>
                            <span>/</span>
                            <span>{project.MaxUsers}</span>
                        </div>
                    </div>
                    <p>El proyecto está <span class='estado-proyecto-{estadoProyecto}'>{estadoProyecto}</span></p>
                </div>

            </div>

            <div class='info__imagenes'>

                <h3 class='imagenes__titulo'>IMÁGENES DEL PROYECTO</h3>
                <div class='imagenes__img'>
                    <p>Actualmente no hay imágenes para el proyecto. Si estás participando en él, ¡anímate a subir alguna del proceso!</p>
                </div>

            </div>

        </section>";

            projectContainer.InnerHtml = $"{firstRowHtml + secondRowHtml + thirdRowHtml}";

            HtmlGenericControl buttonSubmitContainer = new HtmlGenericControl("div");
            buttonSubmitContainer.Attributes["class"] = "buttonSubmit__container";

            if (userIsConnected && !userIsInProject)
            {
                if (estadoProyecto == "completo")
                {
                    Label lblProjectFull = new Label();
                    btnApuntarse.Visible = false;
                    lblProjectFull.Visible = true;

                    lblProjectFull.ID = "lblProjectFull";
                    lblProjectFull.CssClass = "datos__label";
                    lblProjectFull.Text = "El proyecto está completo...";
                    buttonSubmitContainer.Controls.Add(lblProjectFull);
                }
                else
                    btnApuntarse.Text = "Apuntarse al proyecto";
            }
            else if (userIsConnected && userIsInProject)
            {
                Label lblUserInProject = new Label();
                btnApuntarse.Visible = false;
                lblUserInProject.Visible = true;

                lblUserInProject.ID = "lblUserInProject";
                lblUserInProject.CssClass = "datos__label";
                lblUserInProject.Text = "Ya estás participando en este proyecto";

                btnDesapuntarse.Visible = true;

                buttonSubmitContainer.Controls.Add(lblUserInProject);
                buttonSubmitContainer.Controls.Add(btnDesapuntarse);
            }
            else
                btnApuntarse.Text = "Inicia sesión para apuntarte a proyectos";

            buttonSubmitContainer.Controls.Add(btnApuntarse);

            projectContainer.Controls.Add(buttonSubmitContainer);

            project__Container.Controls.Add(projectContainer);
        }


        private string GetCategoryIcon(int category)
        {
            switch (category)
            {
                case 1: return "<i class='fa-solid fa-gamepad'></i>";
                case 2: return "<i class='fa-solid fa-screwdriver-wrench'></i>";
                case 3: return "<i class='fa-solid fa-database'></i>";
                case 4: return "<i class='fa-solid fa-apple-whole'></i>";
                case 5: return "<i class='fa-solid fa-stethoscope'></i>";
                default: return "";
            }
        }

        protected void btnApuntarse_Click(object sender, EventArgs e)
        {
            projectId = Convert.ToInt32(Request.QueryString["id"]);
            user = (User)Session["connectedUser"];
            userId = user.UserId;

            if (user != null)
            {
                dalProjects.InsertNewUserIntoProject(projectId, userId);
                Response.Redirect($"VerProyecto?id={projectId}");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnDesapuntarse_Click(object sender, EventArgs e)
        {
            projectId = Convert.ToInt32(Request.QueryString["id"]);
            user = (User)Session["connectedUser"];
            userId = user.UserId;

            dalProjects.DeleteUserFromProject(projectId, userId);
            Response.Redirect($"VerProyecto?id={projectId}");
        }
    }
}