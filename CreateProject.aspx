<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="CodeLinker.CreateProject" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/crear-proyecto.css" />
    <main class="crear-proyecto">

        <h1 class="crear-proyecto__titulo">&lt;CREAR PROYECTO&gt;</h1>

        <section class="crear-proyecto__contenido">

            <div class="crear-proyecto__tarjeta">

                <div class="crear-proyecto__formulario" id="form-paso-1">

                    <h2 class="formulario__titulo">PASO 1: DEFINE TU PROYECTO</h2>

                    <div class="newProject__Name">
                        <asp:Label ID="lblProjectName" runat="server" Text="Nombre del proyecto:" CssClass="formulario__parrafo-guia"></asp:Label>
                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="newProject__ProgrammingLanguage">
                        <asp:Label ID="lblProgrammingLanguage" runat="server" Text="Lenguaje principal de programación:"  CssClass="formulario__parrafo-guia"></asp:Label>
                        <asp:DropDownList ID="comboProgrammingLanguage" runat="server" CssClass="input">
                        </asp:DropDownList>
                    </div>
                    <div class="newProject__Category">
                        <asp:Label ID="lblCategory" runat="server" Text="Categoría del proyecto:" CssClass="formulario__parrafo-guia"></asp:Label>
                        <asp:DropDownList ID="comboCategory" runat="server" CssClass="input">
                        </asp:DropDownList>
                    </div>
                    <div class="newProject__MaxParticipants">
                        <asp:Label ID="lblMaxParticipantsNumber" runat="server" Text="Número máximo de participantes (de 2 a 5):" CssClass="formulario__parrafo-guia"></asp:Label>
                        <asp:DropDownList ID="comboMaxUsers" runat="server" CssClass="input">
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="newProject__Discord">
                        <p>Creemos en la comunicación para el equipo, ¡utilicemos Discord para fomentarla! Si no sabes utilizar Discord, puedes aprender a crear un servidor una vez tienes tu cuenta <a href="aprende_discordServer.aspx">aquí</a></p>
                       <asp:Label ID="lblDiscord" runat="server" Text="Tu servidor de Discord: " class="formulario__parrafo-guia"></asp:Label>
                       <asp:TextBox ID="urlDiscord" runat="server"></asp:TextBox>
                    </div>
                    <div class="newProject__ShortDesc">
                        <asp:Label ID="lblShortDesc" runat="server" Text="Descripción corta (máximo 400 caracteres):" CssClass="formulario__parrafo-guia"></asp:Label>
                        <asp:TextBox ID="txtShortDesc" runat="server" TextMode="MultiLine" MaxLength="400" CssClass="input" Height="100px" Width="400px"></asp:TextBox>
                    </div>
                    <div class="newProject__Dates">
                        <div class="newProject__startDate">
                            <asp:Label ID="lblStartDate" runat="server" Text="Fecha de inicio:" CssClass="formulario__parrafo-guia"></asp:Label>
                            <asp:TextBox ID="startDate" runat="server" type="date" CssClass="input"></asp:TextBox>
                        </div>
                         <div class="newProject__limitDate">
                            <asp:Label ID="lblLimitDate" runat="server" Text="Fecha límite de finalización:" type="date" CssClass="formulario__parrafo-guia"></asp:Label>
                            <asp:TextBox ID="limitDate" runat="server" type="date" CssClass="input"></asp:TextBox>
                        </div>
                    </div>

                    <div class="formulario__botones-container formulario__botones-container--paso-1">
                        <div class="botones-container__boton boton-continuar">
                            <button type="button">Continuar</button>
                            <i class="fa-solid fa-greater-than"></i>
                        </div>
                    </div>

                </div>

                <div class="crear-proyecto__formulario ocultar" id="form-paso-2">

                    <h2 class="formulario__titulo">PASO 2: TU REPOSITORIO DE GITHUB</h2>

                    <p class="formulario__parrafo-guia">1. Inicia sesión en tu cuenta de GitHub o crea una nueva cuenta.</p>
                    <p class="formulario__parrafo-guia">2. Dirígete a tu perfil de GitHub: Haz click en tu foto de perfil en la esquina superior derecha de la página y selecciona “Tu perfil”.</p>
                    <p class="formulario__parrafo-guia">3. Crea un nuevo repositorio: En tu perfil, haz click en el botón verde “Nuevo” que se encuentra a la derecha de la lista de repositorios.</p>
                    <div class="formulario__parrafo-guia">
                        <p>4. Completa la información del repositorio:</p>
                        <ul>
                            <li>- Nombre del repositorio</li>
                            <li>- Descripción</li>
                            <li>- Visibilidad del repositorio (público o privado)</li>
                        </ul>
                    </div>
                    <p class="formulario__parrafo-guia">5. Crea el repositorio: Una vez hayas completado toda la información, haz click en el botón “Crear repositorio” para crear tu repositorio de GitHub.</p>
                    <p class="formulario__parrafo-guia">6. Copia el enlace del repositorio:</p>
                    <input type="text" class="formulario__input">

                    <div class="formulario__botones-container">
                        <div class="botones-container__boton boton-atras">
                            <i class="fa-solid fa-less-than"></i>
                            <button type="button">Atras</button>
                        </div>
                        <div class="botones-container__boton boton-continuar">
                            <button type="button">Continuar</button>
                            <i class="fa-solid fa-greater-than"></i>
                        </div>
                    </div>

                </div>

                <div class="crear-proyecto__formulario ocultar" id="form-paso-3">

                    <h2 class="formulario__titulo">PASO 3: EXPLICA TU PROYECTO</h2>

                    <p>Ahora que tienes las bases de tu proyecto creadas, necesitaremos que nos des un breve resumen del proyecto y otra que será la descripción completa del mismo, 
                        ¡así podremos mostrarsela a los demás usuarios interesados en unirse!</p>
                    <p>Si quieres utilizar lo que escribas aquí como una plantilla para el archivo README.md de tu repositorio en GitHub, te invitamos a hacerlo. 
                        Si no sabes hacerlo, te lo explicamos <a href="#" target="_blank">aquí.</a></p>
                    <div class="newProject__FullDesc">
                        <asp:Label ID="lblFullDescription" runat="server" Text="Descripción completa (máximo 1200 caracteres):" class="formulario__parrafo-guia"></asp:Label>
                        <asp:TextBox ID="txtFullDesc" runat="server" TextMode="MultiLine" CssClass="input" MaxLength="1200"></asp:TextBox>
                    </div>

                    <div class="formulario__botones-container">
                        <div class="botones-container__boton boton-atras">
                            <i class="fa-solid fa-less-than"></i>
                            <button type="button">Atras</button>
                        </div>
                        <div class="botones-container__boton boton-continuar">
                            <button type="button">Finalizar</button>
                            <i class="fa-solid fa-greater-than"></i>
                        </div>
                    </div>

                </div>

                <div class="tarjeta__paso tarjeta__paso-1 active" id="paso-1">PASO 1</div>
                <div class="tarjeta__paso tarjeta__paso-2--hidden" id="paso-2">PASO 2</div>
                <div class="tarjeta__paso tarjeta__paso-3--hidden" id="paso-3">PASO 3</div>

            </div>

        </section>

    </main>
    <script>
        const formPaso1 = document.getElementById('form-paso-1')
        const formPaso2 = document.getElementById('form-paso-2')
        const formPaso3 = document.getElementById('form-paso-3')
        const paso1 = document.getElementById('paso-1')
        const paso2 = document.getElementById('paso-2')
        const paso3 = document.getElementById('paso-3')
        const botonesContinuar = document.querySelectorAll('.boton-continuar')
        const botonesAtras = document.querySelectorAll('.boton-atras')

        function mostrarPaso1() {
            paso1.classList.add('tarjeta__paso-1')
            paso1.classList.remove('tarjeta__paso-1--hidden')
        }

        function ocultarPaso1() {
            paso1.classList.add('tarjeta__paso-1--hidden')
            paso1.classList.remove('tarjeta__paso-1')
        }

        function mostrarPaso2() {
            paso2.classList.add('tarjeta__paso-2')
            paso2.classList.remove('tarjeta__paso-2--hidden')
        }

        function ocultarPaso2() {
            paso2.classList.add('tarjeta__paso-2--hidden')
            paso2.classList.remove('tarjeta__paso-2')
        }

        function mostrarPaso3() {
            paso3.classList.add('tarjeta__paso-3')
            paso3.classList.remove('tarjeta__paso-3--hidden')
        }

        function ocultarPaso3() {
            paso3.classList.add('tarjeta__paso-3--hidden')
            paso3.classList.remove('tarjeta__paso-3')
        }

        botonesContinuar.forEach(boton => {
            boton.addEventListener('click', () => {
                if (paso1.classList.contains('active')) {
                    paso1.classList.remove('active')
                    paso2.classList.add('active')

                    ocultarPaso1()
                    mostrarPaso2()

                    formPaso1.classList.add('ocultar')
                    formPaso2.classList.remove('ocultar')
                } else if (paso2.classList.contains('active')) {
                    paso2.classList.remove('active')
                    paso3.classList.add('active')

                    ocultarPaso2()
                    mostrarPaso3()

                    formPaso2.classList.add('ocultar')
                    formPaso3.classList.remove('ocultar')
                }
            })
        })

        botonesAtras.forEach(boton => {
            boton.addEventListener('click', () => {
                if (paso2.classList.contains('active')) {
                    paso2.classList.remove('active')
                    paso1.classList.add('active')

                    ocultarPaso2()
                    mostrarPaso1()

                    formPaso2.classList.add('ocultar')
                    formPaso1.classList.remove('ocultar')
                } else if (paso3.classList.contains('active')) {
                    paso3.classList.remove('active')
                    paso2.classList.add('active')

                    ocultarPaso3()
                    mostrarPaso2()

                    formPaso3.classList.add('ocultar')
                    formPaso2.classList.remove('ocultar')
                }
            })
        })

    </script>
</asp:Content>
