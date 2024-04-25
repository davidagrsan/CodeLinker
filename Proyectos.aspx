<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proyectos.aspx.cs" Inherits="CodeLinker.Proyectos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <main class="proyectos">
            <h1>&#60;Proyectos&#62;</h1>
            <section id="project__filters">
                <div>
                        <asp:Button ID="butClean" runat="server" Text="Limpiar filtros" />
                </div>
                <div id="project__CheckBoxes">
                        <asp:CheckBox ID="checkOpen" runat="server" AutoPostBack="True"/>
                        <asp:Label ID="lblCheckOpen" runat="server" Text="Proceso abierto"></asp:Label> 
                        <asp:CheckBox ID="checkEnded" runat="server" AutoPostBack="True" />
                        <asp:Label ID="lblCheckEnded" runat="server" Text="Solo finalizados"></asp:Label>
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
            <section id="project__projects" runat="server">
            </section>
        </main>
</asp:Content>
