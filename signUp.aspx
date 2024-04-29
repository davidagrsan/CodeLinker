<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="CodeLinker.SignUp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/login.css" />
    <main class="signup">
        <div id="signUp__header">
            <a href="Default.aspx"><img src="Content/img/header-logo.png" alt="Logo" class="navbar__logo"></a>
        </div>
        <div id="signUp__form">
            <asp:Label ID="lblCreationStatus" CssClass="connected" runat="server"></asp:Label>
            <label for="signUpName"><b>Usuario</b></label><br />
            <asp:TextBox ID="txtBoxUser" runat="server" placeholder="Usuario"></asp:TextBox>
            <label for="signUpEmail"><b>Email</b></label><br />
            <asp:TextBox ID="txtBoxEmail" runat="server" placeholder="Email"></asp:TextBox>
            <label for="pwd"><b>Contraseña</b></label><br>
            <asp:TextBox ID="txtBoxPwd" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            <label for="cnf"><b>Confirmar contraseña</b></label><br>
            <asp:TextBox ID="txtBoxConfirmPwd" runat="server" TextMode="Password" placeholder="Confirma la contraseña"></asp:TextBox>
            <div id="signUp__submit">
                <asp:Button ID="btnRegister" runat="server" Text="Registrate" OnClick="btnRegister_Click" />
            </div>
            <div id="signUp__toLogin">
                <p>¿Ya tienes una cuenta?</p>
                <a href="Login.aspx">Inicia sesión</a>
            </div>
        </div>
    </main>

</asp:Content>
