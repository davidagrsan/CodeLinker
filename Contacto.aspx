<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="CodeLinker.Contacto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/contacto.css"/>

    <main class="contacto">

        <h1 class="contacto__titulo">&lt;Contacta con nosotros&gt;</h1>

        <section class="contacto__informacion">

            <div class="informacion__formulario">
                <div class="formulario__contenedor">
                    <article class="formulario__campo">
                        <label class="campo__label" for="nombre">Dinos cómo te llamas:</label>
                        <input class="campo__input" type="text" id="nombre" placeholder="Escribe aquí tu nombre...">
                    </article>

                    <article class="formulario__campo">
                        <label class="campo__label" for="email">Tu correo electrónico</label>
                        <input class="campo__input" type="email" id="email" placeholder="Escribe aquí tu correo electrónico...">
                    </article>

                    <article class="formulario__campo">
                        <label class="campo__label" for="mensaje">¿En que podemos ayudarte?</label>
                        <textarea class="campo__text" id="mensaje" placeholder="Escribe aquí tu mensaje para nosotros..."></textarea>
                    </article>
                    
                    <button class="formulario__boton" type="button">Enviar</button>
                </div>

            </div>

            <div class="informacion__descripcion">
                <p class="description__parrafo">Para nosotros es muy importante saber lo que tienes que decir. Puedes contar con nosotros para lo que necesites.</p>
                <p class="description__parrafo">Estamos aquí para ayudarte en cualquier proyecto, resolver tus dudas o simplemente charlar sobre tus ideas.</p>
                <p class="description__parrafo">No dudes en ponerte en contacto con nuestro atento equipo para cualquier duda o solicitud que tengas. Incluso tu feedback es fundamental para nosotros y será escuchado y apreciado.</p>
                <p class="description__parrafo">Estamos ansiosos por colaborar contigo para hacer realidad tus objetivos en el mundo de la tecnología y el desarrollo.</p>
                <p class="description__parrafo">¡Queremos y necesitamos escucharte! :&#41;</p>
            </div>

        </section>
    </main>
</asp:Content>
