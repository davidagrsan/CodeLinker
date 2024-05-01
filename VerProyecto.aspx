<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerProyecto.aspx.cs" Inherits="CodeLinker.VerProyecto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main class="proyecto">
    <link rel="stylesheet" href="css/ver-proyecto.css" />

    <h1 class="proyecto__titulo">&lt;PROYECTO&gt;</h1>

    <div id="project__Container" runat="server">
        <div>
            <asp:Button ID="btnApuntarse" runat="server" CssClass="datos__boton" Text="" OnClick="btnApuntarse_Click" />
            <asp:Button ID="btnDesapuntarse" runat="server" CssClass="datos__boton" Text="¿Desapuntarse del proyecto?" OnClick="btnDesapuntarse_Click" Visible="False" />
        </div>
        
    </div>

</main>

</asp:Content>
