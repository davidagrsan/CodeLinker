<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="CodeLinker.Contacto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/sobre-nosotros.css"/>
    <main class="sobre-nosotros">

        <h1 class="sobre-nosotros__titulo">&lt;SOBRE NOSOTROS&gt;</h1>

        <section class="sobre-nosotros__info">

            <article>
                <h2 class="info__titulo">Conoce nuestra <span class="info__datos__titulo--violet">historia</span></h2>
                <p class="info__parrafo">Nuestra historia empezó durante el mismo curso en el que nos conocimos. Un día cualquiera, compartiendo nuestras vidas, nos dimos cuenta de que todos nos encontrábamos en la misma situación. Por mucho que estudiáramos, los ejercicios de clase y los proyectos que las empresas realizan tienen dos niveles de conocimiento totalmente distintos. Nos sentíamos que con lo que aprendíamos en clase nunca estaríamos preparados para un trabajo de programador de verdad. Justamente esta sensación, este gran vacío que sentíamos entre estos dos mundos nos inspiró y nos dio la motivación necesaria para empezar CodeLinker y crear un puente para que los futuros jóvenes que quieran aprender a programar, sea en el lenguaje que sea, puedan ganar una experiencia más realista de lo que es ser un programador.</p>
            </article>

            <article>
                <h2 class="info__titulo">Conoce nuestra <span class="info__datos__titulo--violet">proyección</span></h2>
                <p class="info__parrafo">Nuestra misión es ayudar a las personas que están buscando trabajo como programadores a obtener una experiencia más real del tipo de proyectos que se realizan en el mundo profesional. Apoyándolos y motivándolos a buscar la excelencia en su camino para terminar realizando sus propios proyectos o entendiendo que es lo que las empresas buscan y necesitan de sus trabajadores.</p>
            </article>

            <article>
                <h2 class="info__titulo">Conoce nuestro <span class="info__datos__titulo--violet">equipo</span></h2>
                <p class="info__parrafo">El equipo de CodeLinker está formado por cuatro alumnos de Fundació Esplai del curso de .Net.</p>
            </article>

        </section>

        <section class="sobre-nosotros__equipo">

            <article class="equipo__integrante">
                <img class="integrante__imagen-izq" src="Content/img/MatteoImage.png" alt="Foto Matteo">
                <span>Matteo Zhao</span>
            </article>

            <article class="equipo__integrante">
                <img class="integrante__imagen-der" src="Content/img/DavidImage.png" alt="Foto David">
                <span>David Agredano Sánchez</span>
            </article>

            <article class="equipo__integrante">
                <img class="integrante__imagen-izq" src="Content/img/AlbertoImage.png" alt="Foto Alberto">
                <span>Alberto Sánchez Pita</span>
            </article>

            <article class="equipo__integrante">
                <img class="integrante__imagen-der" src="Content/img/BernatImage.png" alt="Foto Bernat">
                <span>Bernat Codern Cortada</span>
            </article>

        </section>
    </main>
</asp:Content>