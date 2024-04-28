<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CodeLinker._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/home.css" />
    <main class="home">

        <h2 class="home__titulo">Aquí es donde empieza tu aventura.<br>
            Atrevete a ser #Code<span class="home__titulo--violet">Linker</span></h2>

        <section class="home__description">

            <p class="description__text">Somos tu nexo para pasar de la teoría a la acción, un puente que te lleva de la exploración al dominio y que con cada línea de código te acercamos un paso más hacia tus metas.</p>
            <p class="description__text">Únete a una comunidad que transformará el aprendizaje en una emocionante aventura. Te ayudaremos a convertir tus sueños de desarrollador en realidad.</p>

            <div class="description__saber-mas">
                <p class="saber-mas__text">¿Quieres saber más sobre nosotros?</p>
                <i class="fa-solid fa-arrow-down saber--mas__icon"></i>

                <div class="saber-mas__logo-container">
                    <a href="AboutUs.aspx"><img class="saber-mas__logo" src="Content/img/logo-without-letters.png" alt="Logo"></a>
                </div>

            </div>

        </section>

        <section class="home__botones">
            <asp:Button ID="btn_createProject" runat="server" Text="Crea tu proyecto" OnClick="btn_createProject_Click" />
            <asp:Button ID="btn_joinProject" runat="server" Text="Únete a un proyecto" OnClick="btn_joinProject_Click" />
        </section>
    </main>

</asp:Content>
