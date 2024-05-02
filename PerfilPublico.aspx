<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilPublico.aspx.cs" Inherits="CodeLinker.PerfilPublico" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/perfil.css">
    <main class="perfilPublico">

        <section class="perfil__usuario" id="perfil">

            <h1 class="usuario__titulo">&lt;Perfil&gt;</h1>

            <div class="usuario__datos">
                <article class="datos__dato">
                    <label for="username">Nombre de usuario</label>
                    <asp:Label ID="username" runat="server"></asp:Label>
                </article>

                <article class="datos__dato">
                    <label for="nombre">Nombre</label>
                    <asp:Label ID="nombre" runat="server"></asp:Label>
                </article>

                <article class="datos__dato">
                    <label for="apellido">Apellido</label>
                    <asp:Label ID="apellido" runat="server"></asp:Label>
                </article>

                <article class="datos__dato">
                    <label for="email">Email</label>
                    <asp:Label ID="email" runat="server"></asp:Label>
                </article>

                <article class="datos__dato">
                    <label for="telefono">Telefono</label>
                    <asp:Label ID="telefono" runat="server"></asp:Label>
                </article>

                <article class="datos__dato">
                    <label for="fecha">Fecha de nacimiento</label>
                    <asp:Label ID="fecha" runat="server" TextMode="Date"></asp:Label>
                </article>


            </div>

            <div class="usuario__especialidad">

                <div class="usuario__logo">
                    <img class="logo__img" id="fotoPerfil" src="" alt="Foto usuario" runat="server">
                    <%--<img class="logo__logo" src="Content/img/logo-without-letters.png" alt="Logo Codelinkers">--%>
                </div>
                <div class="especialidad__datos">
                    <h3 class="especialidad__titulo">Especialidad</h3>
                    <div class="especialidad__container">
                        <article>
                            <asp:RadioButton ID="frontend" runat="server" GroupName="especialidad" Enabled="False" />
                            <asp:Label ID="frontendLabel" runat="server" Text="Front-End" AssociatedControlID="frontend"></asp:Label>
                        </article>

                        <article>
                            <asp:RadioButton ID="backend" runat="server" GroupName="especialidad" Enabled="False" />
                            <asp:Label ID="backendLabel" runat="server" Text="Back-End" AssociatedControlID="backend"></asp:Label>
                        </article>

                        <article>
                            <asp:RadioButton ID="fullstack" runat="server" GroupName="especialidad" Enabled="False" />
                            <asp:Label ID="fullstackLabel" runat="server" Text="Full Stack" AssociatedControlID="fullstack"></asp:Label>
                        </article>
                    </div>
                </div>
        </section>
        <div id="linksContainer" runat="server">
            <div>
                <i class="fa-brands fa-linkedin"></i>
                <asp:HyperLink ID="linkLinkedIn" runat="server"></asp:HyperLink>
            </div>
            <div>
                <i class="fa-brands fa-github"></i>
                <asp:HyperLink ID="linkGitHub" runat="server"></asp:HyperLink>
            </div>
        </div>
    </main>
</asp:Content>
