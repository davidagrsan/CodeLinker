<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerProyecto.aspx.cs" Inherits="CodeLinker.VerProyecto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main class="proyecto">
    <link rel="stylesheet" href="css/ver-proyecto.css" />

    <h1 class="proyecto__titulo">&lt;PROYECTO&gt;</h1>

    <div class="proyecto__container">

        <section class="container__encabezado">
            <div class="encabezado__tipo-proyecto">
                <i class="fa-solid fa-gamepad"></i>
                <span class="encabezado__lenguaje">C#</span>
            </div>
            <h2 class="encabezado__titulo">&lt;Ahorcado en C#&gt;</h2>
            <span class="encabezado__codelinker">CodeLinker</span>
        </section>

        <section class="container__descripcion">
            <p class="descripcion__texto">Bienvenidos al desafío del juego del ahorcado en C# para consola, donde podréis demostrar vuestras habilidades de programación desarrollando una versión del clásico juego del ahorcado diseñada para ser ejecutada en la línea de comandos. 

                Este proyecto te ofrece la oportunidad de sumergirte en el mundo de la programación de aplicaciones de consola, aplicar conceptos fundamentales de programación en C# y crear una experiencia de juego divertida y desafiante sin la necesidad de una interfaz gráfica.
                    
                Implementación de la Lógica del Juego: Utilizad vuestros conocimientos en C# para implementar la lógica del juego del ahorcado en una aplicación de consola. Esto incluye la generación de palabras aleatorias, la gestión de las letras ingresadas por el jugador y la actualización de la interfaz de texto para reflejar el estado del juego.
                    
                Personalización de la Experiencia de Juego: Proporcionad opciones para personalizar la experiencia de juego, como la dificultad del juego, el número de intentos permitidos y la selección de categorías de palabras. Permitid a los jugadores ajustar la configuración según sus preferencias individuales para una experiencia de juego más personalizada.
                    
                Ten en cuenta los posibles errores: Acuérdate que quien interactúa con el programa es un usuario impredecible, y pueden ocurrir bugs inesperados. Intentad tener en cuenta todas las posibles interacciones.
            </p>
        </section>

        <section class="container__info">

            <div class="info__datos">
                <div class="datos__fechas">
                    <div class="fechas__comienzo">
                        <p>Fecha comienzo</p>
                        <span>10/05/24</span>
                    </div>
                    <i class="fa-solid fa-arrow-right"></i>
                    <div class="fechas__limite">
                        <p>Fecha límite</p>
                        <span>31/05/24</span>
                    </div>
                </div>

                <div class="datos__miembros">
                    <p>Actualmente hay</p>
                    <div class="miembros__cantidad">
                        <i class="fa-solid fa-users"></i>
                        <div class="cantidad__num-miembros">
                            <span>2</span>
                            <span>/</span>
                            <span>3</span>
                        </div>
                    </div>
                    <p>El proyecto está <span class="datos__estado">abierto</span></p>
                </div>

                <asp:Button ID="btnApuntarse" CssClass="datos__boton" runat="server" Text="Apuntarse al proyecto" />
            </div>

            <div class="info__imagenes">

                <h3 class="imagenes__titulo">IMÁGENES DEL PROYECTO</h3>
                <div class="imagenes__img">
                    <p>Actualmente no hay imágenes para el proyecto. Si estás participando en él, ¡anímate a subir alguna del proceso!</p>
                </div>

            </div>

        </section>

    </div>

</main>

</asp:Content>
