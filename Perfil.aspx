﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="CodeLinker.Contacto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/perfil.css">
    <main class="perfil">
        
        <nav class="perfil__ancors">
            <ul class="ancors__lista">
                <li class="ancors__elemento"><a href="#perfil">Perfil</a></li>
                <li class="ancors__elemento"><a href="#privacidad">Privacidad</a></li>
                <li class="ancors__elemento"><a href="#cuenta">Gestionar cuenta</a></li>
            </ul>
        </nav>
    
        <section class="perfil__usuario" id="perfil">
    
            <h1 class="usuario__titulo">&lt;Perfil&gt;</h1>
    
            <div class="usuario__datos">
    
                <article class="datos__dato">
                    <label for="nombre">Nombre</label>
                    <asp:TextBox ID="nombre" runat="server"></asp:TextBox>
                </article>
    
                <article class="datos__dato">
                    <label for="apellido">Apellido</label>
                    <asp:TextBox ID="apellido" runat="server"></asp:TextBox>
                </article>
    
                <article class="datos__dato">
                    <label for="email">Email</label>
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                </article>
    
                <article class="datos__dato">
                    <label for="telefono">Telefono</label>
                    <asp:TextBox ID="telefono" runat="server"></asp:TextBox>
                </article>
    
                <article class="datos__dato">
                    <label for="fecha">Fecha de nacimiento</label>
                    <asp:TextBox ID="fecha" runat="server" TextMode="Date"></asp:TextBox>
                </article>
    
                <article class="datos__dato">
                    <label for="linkedin">LinkedIn</label>
                    <asp:TextBox ID="linkedin" runat="server"></asp:TextBox>
                </article>
    
                <article class="datos__dato">
                    <label for="github">GitHub</label>
                    <asp:TextBox ID="github" runat="server"></asp:TextBox>
                </article>
                
            </div>
    
            <div class="usuario__especialidad">
    
                <div class="usuario__logo">
                    <img class="logo__img" src="Content/img/img-alberto.png" alt="Foto usuario">
                    <img class="logo__logo" src="Content/img/logo-without-letters.png" alt="Logo Codelinkers">
                </div>
    
                <div class="especialidad__datos">
                    <h3 class="especialidad__titulo">Especialidad</h3>
                    <div class="especialidad__container">
    
                        <article>
                            <asp:RadioButton ID="frontend" runat="server" GroupName="especialidad" />
                            <asp:Label ID="frontendLabel" runat="server" Text="Front-End" AssociatedControlID="frontend"></asp:Label>
                        </article>
    
                        <article>
                            <asp:RadioButton ID="backend" runat="server" GroupName="especialidad" />
                            <asp:Label ID="backendLabel" runat="server" Text="Back-End" AssociatedControlID="backend"></asp:Label>
                        </article>
                        
                        <article>
                            <asp:RadioButton ID="fullstack" runat="server" GroupName="especialidad" />
                            <asp:Label ID="fullstackLabel" runat="server" Text="Full Stack" AssociatedControlID="fullstack"></asp:Label>
                        </article>
    
                    </div>
                </div>
    
            </div>
    
        </section>

        <section class="perfil__privacidad" id="privacidad">

            <h2 class="privacidad__titulo">&lt;Privacidad&gt;</h2>

            <h3 class="privacidad__subtitulo">Quieres que su perfil sea <span class="privacidad__subtitulo--violet">privado</span> o <span class="privacidad__subtitulo--violet">publico?</span></h3>
            <p>Al seleccionar la opción “On” en la casilla de abajo, bloquearemos su informació personal a los visitantes de su perfil personal.</p>
            <p>Solamente mostraremos su nombre y su foto de perfil.</p>
            <div class="privacidad__activar">
                <p>Activar modo privado</p>
                <div class="privacidad__switch">
                    <div class="switch__option switch__active">Off</div>
                    <div class="switch__option">On</div>
                </div>
            </div>

            <h3 class="privacidad__subtitulo">Quieres modificar tu <span class="privacidad__subtitulo--violet">contraseña</span>?</h3>

            <div class="privacidad__formulario">

                <article class="formulario__campo">
                    <label for="actual-pass">Contraseña actual</label>
                    <input type="password" id="actual-pass">
                </article>

                <article class="formulario__campo">
                    <label for="new-pass">Nueva contraseña</label>
                    <input type="password" id="new-pass">
                </article>

                <article class="formulario__campo">
                    <label for="rep-new-pass">Repetir nueva contraseña</label>
                    <input type="password" id="rep-new-pass">
                </article>

                <button class="privacidad__boton" type="button">Guardar</button>

            </div>

        </section>

        <section class="perfil__gestion" id="cuenta">

            <h3 class="gestion__titulo">&lt;Gestionar de la cuenta&gt;</h3>

            <div class="gestion__botones">
                <button class="privacidad__boton" type="button">Cerrar sesión</button>
                <button class="privacidad__boton" type="button">Eliminar cuenta</button>
            </div>

        </section>

    </main>
    <script>
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault()

                document.querySelector(this.getAttribute('href')).scrollIntoView({
                    behavior: 'smooth'
                })
            })
        })

        const switchOption = document.querySelectorAll('.switch__option')
        switchOption.forEach(e => {
            e.addEventListener('click', () => {
                switchOption.forEach(elem => {
                    elem.classList.remove('switch__active')
                })

                e.classList.add('switch__active')
            })
        })
    </script>

</asp:Content>
