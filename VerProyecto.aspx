<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerProyecto.aspx.cs" Inherits="CodeLinker.VerProyecto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main class="proyecto">
    <link rel="stylesheet" href="css/ver-proyecto.css" />

    <h1 class="proyecto__titulo">&lt;PROYECTO&gt;</h1>

    <div id="project__Container" runat="server">

        <asp:Button ID='btnApuntarse' CssClass='datos__boton' runat='server' Text='Apuntarse al proyecto' />
    </div>

</main>

</asp:Content>
