<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aprende_Github_README.aspx.cs" Inherits="CodeLinker.Contacto" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="/css/aprende.css">
    <main class="aprende gitHubReadme">
     <h1>Guía para README.txt</h1>

     <h2>Básicos</h2>

     <p class="aprende__step">1. Accede al Repositorio en GitHub:</p>

     <ul>
         <li>-Inicia sesión en tu cuenta de GitHub.</li>
         <li>-Dirígete al repositorio en el que deseas crear el archivo README.txt.</li>
     </ul>

     <p class="aprende__step">2. Navega al Editor de Archivos, y una vez dentro del repositorio, haz clic en el botón "Add file" y selecciona "Create new file".</p>

     <p class="aprende__step">3. Nombre y Ubicación del Archivo: En el campo "Name your file...", escribe el nombre README.txt.</p>

     <p class="aprende__step">4. Edición del Archivo:</p>
     <ul>
         <li>-En el área de edición, puedes comenzar a escribir el contenido de tu archivo README.txt.</li>
         <li>-Puedes usar Markdown para dar formato al texto si lo deseas. Por ejemplo:</li>
         <li class="img-tutorial"><img class="tutorial-img" src="/Content/img/github_readme_1.png"></li>
     </ul>

     <p class="aprende__step">5. Previsualización y Comentario Opcional:</p>
     <ul>
         <li>-Puedes previsualizar el contenido del archivo haciendo clic en la pestaña "Preview".</li>
         <li>-Si lo deseas, puedes agregar un comentario en el campo "Commit new file" para describir los cambios que estás realizando.</li>
     </ul>

     <p class="aprende__step">6. ¡Importante! ¡Guarda tus cambios!</p>

     <p>Una vez que estés satisfecho con el contenido del archivo README.txt, haz clic en el botón "Commit new file" para guardar los cambios.</p>

     <h2>Estilos básicos para tu README.txt</h2>

     <p>Recuerda que puedes añadir ciertos elementos que le darán estilo a tu README.txt aplicando algunas normas propias que detecta GitHub de forma automática desde tu documento de texto.</p>

     <p>Te dejamos algunas aquí:</p>

     <img class="tutorial-img" src="/Content/img/github_readme_2.png">
 </main>
</asp:Content>