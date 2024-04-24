<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="CodeLinker.SignUp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/login.css"/>
    <main class="signup">
        <div>
            <div>
                <br />
                <img src="Content/img/header-logo.png" alt="logo CodeLinker">
                <br />
            </div>
            <div>
                <br /><br />
                <asp:Label ID="lblCreationStatus" CssClass="connected" runat="server"></asp:Label>
                <br /><br />
                <label for="loginName"><b>Usuario</b></label><br />
                <asp:TextBox ID="txtBoxUser" runat="server" placeholder="Usuario"></asp:TextBox>
                <br><br>
                <label for="loginEmail"><b>Email</b></label><br />
                <asp:TextBox ID="txtBoxEmail" runat="server" placeholder="Email"></asp:TextBox>
                <br><br>
                <label for="pwd"><b>Contraseña</b></label><br>
                <asp:TextBox ID="txtBoxPwd" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                <br><br>
                <label for="cnf"><b>Confirmar contraseña</b></label><br>
                <asp:TextBox ID="txtBoxConfirmPwd" runat="server" TextMode="Password" placeholder="Confirma la contraseña"></asp:TextBox>
                <br><br>
                <asp:Button ID="btnRegister" runat="server" Text="Registrate" OnClick="btnRegister_Click" />
                <br><br>
                <p>¿Ya tienes una cuenta? <a href="Login.aspx">Inicia sesión</a></p>
            </div>
        </div>
    </main>

</asp:Content>
