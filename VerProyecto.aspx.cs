using CodeLinker.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
                if (int.TryParse(Request.QueryString["id"], out userId))
                {
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
                else
                {
                    Response.Redirect("Default.aspx");
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
            HtmlGenericControl projectData = new HtmlGenericControl("div");
            projectData.Attributes["class"] = "proyecto__container";
            projectData.ID = "projectContainer";
            projectData.EnableViewState = false;

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
                    <div class='metodo__comunicacion'>
                        <a href='{project.CommunicationMethod}'><i class='fa-brands fa-discord'></i></a>
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

            projectData.InnerHtml = $"{firstRowHtml + secondRowHtml + thirdRowHtml}";

            HtmlGenericControl buttonSubmitContainer = new HtmlGenericControl("div");
            buttonSubmitContainer.Attributes["class"] = "buttonSubmit__container";

            // Condicional si el usuario está conectado pero no inscrito al proyecto
            if (userIsConnected && !userIsInProject)
            {
                // En caso de estar completo no podrá inscribirse
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
            // Si está conectado pero está ya en el proyecto, no podrá unirse
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
            // Si no tiene la sesión iniciada, el botón tendrá un texto diferente (y le mandará al login en el evento del click)
            else
                btnApuntarse.Text = "Inicia sesión para apuntarte a proyectos";

            // Añadimos todo lo creado a su contenedor, después a la variable que envuelve el contenedor y después al contenedor ya existente en el HTML
            buttonSubmitContainer.Controls.Add(btnApuntarse);

            // Lógica de creación de los usuarios en el proyecto
            HtmlGenericControl usersHeader = new HtmlGenericControl("h3");
            usersHeader.InnerText = "Usuarios inscritos";
            HtmlGenericControl usersInProjectContainer = new HtmlGenericControl("div");

            usersInProjectContainer.Attributes["class"] = "usuariosInscritos__proyecto";

            LiteralControl usersInProyectList = LoadUsersInProject(projectId);

            usersInProjectContainer.Controls.Add(usersHeader);
            usersInProjectContainer.Controls.Add(usersInProyectList);

            // Añadimos el botón
            projectData.Controls.Add(buttonSubmitContainer);

            // Añadimos los usuarios inscritos
            projectData.Controls.Add(usersInProjectContainer);

            // Se añade todo definitivamente al control en el aspx
            project__Container.Controls.Add(projectData);
        }

        // Dependiendo de la categoría tendrá diferente icono
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

        // Creación aparte de los usuarios en el proyecto
        private LiteralControl LoadUsersInProject(int projectId)
        {
            List<User> usersInsideProject = dalProjects.ListUsersInProject(projectId);

            // Construir la lista HTML
            StringBuilder userListHtml = new StringBuilder();
            userListHtml.Append("<ul>");

            foreach (User user in usersInsideProject)
            {
                userListHtml.Append($"<li><a href='PerfilPublico.aspx?id={user.UserId}'>{user.UserName}</a></li>");
            }

            userListHtml.Append("</ul>");

            return new LiteralControl(userListHtml.ToString());
        }

        protected void btnApuntarse_Click(object sender, EventArgs e)
        {
            projectId = Convert.ToInt32(Request.QueryString["id"]);
            user = (User)Session["connectedUser"];

            if (user != null)
            {
                userId = user.UserId;
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