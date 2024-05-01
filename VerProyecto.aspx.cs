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

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["connectedUser"];
            if (user != null)
            {
                if (!IsPostBack)
                {
                    projectId = Convert.ToInt32(Request.QueryString["id"]);
                    userId = user.UserId;
                    LoadProjectInfo(projectId);
                }
            }
        }

        private void LoadProjectInfo(int projectId)
        {
            // Cargamos los datos del proyecto de la base de datos
            Project project = dalProjects.LoadSingleProject(projectId);

            int actualParticipants = dalProjects.CountParticipants(project.ProjectId);
            string estadoProyecto = project.MaxUsers < actualParticipants ? "abierto" : "cerrado";

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
                    <i class=fa-solid fa-arrow-right'></i>
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

            projectContainer.InnerHtml = $"{firstRowHtml}";
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
            dalProjects.InsertNewUserIntoProject(projectId, userId);
        }
    }
}