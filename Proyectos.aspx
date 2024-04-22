<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proyectos.aspx.cs" Inherits="CodeLinker.Proyectos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <main class="proyectos">
            <h1>&#60;Proyectos&#62;
        </h1>
            <section id="project__filters">
                <asp:Button ID="butClean" runat="server" Text="Limpiar filtros" />
                <div id="project__CheckBoxes">
                    <div>
                        <asp:CheckBox ID="checkOpen" runat="server" />
                        <asp:Label ID="lblCheckOpen" runat="server" Text="Proceso abierto"></asp:Label>
                    </div>
                   <div>
                        <asp:CheckBox ID="checkEnded" runat="server" />
                        <asp:Label ID="lblCheckEnded" runat="server" Text="Solo finalizados"></asp:Label>
                    </div>
                    <div>
                    </div>
                </div>
                <div id="project__DropDownLists">
                    <div>
                        <asp:DropDownList ID="comboProgrammingLanguage" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:DropDownList ID="comboType" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:DropDownList ID="comboCategory" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </section>
            <section id="project__projects">
                <div class="project__container">
                    <div class="project__firstRow">
                        <p class="project__language">C#</p>
                        <p class="project__lblStartDate">Fecha comienzo: <span class="project__numStartDate">10/05/2024</span></p>
                        <p class="project__type"><i class="fa-solid fa-gamepad"></i></p>
                        <p class="project__lblEndDate">Fecha límite: <span class="project__numEndDate">31/05/2024</span></p>
                        <p class="project__category">CodeLinker</p>
                    </div>
                    <div class="project__secondRow">
                        <h2 class="project__title">AHORCADO EN C#</h2>
                        <p class="project__shortDesc">Desarrolla el clásico juego del ahorcado en C#.<br/> Utiliza tus habilidades de programación para implementar la lógica del juego, ¡demostrad vuestra destreza técnica mientras construyes una experiencia de juego divertida y desafiante!</p>
                    </div>
                    <div class="project__thirdRow">
                        <div class="project__participantsContainer">
                            <i class="fa-solid fa-users"></i>
                            <div>
                                <p class="project__actualParticipants">2</p>
                                <span class="separator">/</span>
                                <p class="project__maxParticipants">3</p>
                            </div>
                        </div>
                        <button class="project__moreInfo">Ver más</button>
                        <div class="project__state">
                            <span class="project__open">Abierto</span>
                            <span class="project__running">En progreso</span>
                        </div>
                    </div>
                </div>
            </section>
        </main>
</asp:Content>
