<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="CodeLinker.Projects" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/proyectos.css"/>
        <main class="proyectos">
            <h1>&#60;Proyectos&#62;</h1>
            <section id="project__filters">
                <div>
                        <asp:Button ID="butClean" runat="server" Text="Limpiar filtros" OnClick="butClean_Click" />
                </div>
                <div id="project__CheckBoxesFirst">
                        <asp:CheckBox ID="checkOpen" runat="server" AutoPostBack="True"/>
                        <asp:Label ID="lblCheckOpen" runat="server" Text="Proceso abierto"></asp:Label> 
                </div>
                <div id="project__CheckBoxesSecond">
                        <asp:CheckBox ID="checkEnded" runat="server" AutoPostBack="True" />
                        <asp:Label ID="lblCheckEnded" runat="server" Text="Mostrar finalizados"></asp:Label>
                </div>
                <div id="project__DropDownLists">
                    <div>
                        <asp:DropDownList ID="comboProgrammingLanguage" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:DropDownList ID="comboType" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:DropDownList ID="comboCategory" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
            </section>
            <section id="project__projects" runat="server">
            </section>

            <script>
                // Script para eliminar datos innecesarios una vez el proyecto está acabado (se ha de hacer automático con la BBDD)
                const projectContainers = document.querySelectorAll(".project__container");

                projectContainers.forEach(container => {
                    const finishedState = container.querySelector(".finished");
                    const firstRow = container.querySelector(".project__firstRow");
                    const participantsContainer = container.querySelector(".project__participantsContainer");
                    const openState = container.querySelector(".project__open");
                    const closedState = container.querySelector(".project__closed");

                    if (finishedState) {
                        if (firstRow) {
                            const startDate = firstRow.querySelector(".project__lblStartDate");
                            const endDate = firstRow.querySelector(".project__lblEndDate");

                            if (startDate) {
                                startDate.style.display = "none";
                            }
                            if (endDate) {
                                endDate.style.display = "none";
                            }
                        }
                        if (participantsContainer) {
                            participantsContainer.style.display = "none";
                        }
                        if (openState) {
                            openState.style.display = "none";
                        }
                        if (closedState) {
                            closedState.style.display = "none";
                        }
                    }
                });
            </script>
        </main>
</asp:Content>
