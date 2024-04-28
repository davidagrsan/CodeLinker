<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CodeLinker.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/login.css" />
    <main class="login">
        <div id="login__header">
            <a href="Default.aspx"><img src="Content/img/header-logo.png" alt="Logo" class="navbar__logo"></a>
        </div>
        <div id="login__form">
            <asp:Label ID="lblConnectedLogIn" CssClass="connected" runat="server"></asp:Label>
            <label for="loginName"><b>Usuario o Correo</b></label><br />
            <asp:TextBox ID="txtBoxUserLogIn" runat="server" placeholder="Usuario o Correo"></asp:TextBox>
            <label for="pwd"><b>Contraseña</b></label><br>
            <asp:TextBox ID="txtBoxPwdLogIn" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            <div id="login__submit">
                <asp:Button ID="btn__login" runat="server" Text="Iniciar sesión" OnClick="btnLogin_Click" />
            </div>
            <div id="login__forgotPassword">
                <p><a href="#" id="forgotPasswordLink">¿Has olvidado tu contraseña?</a></p>
            </div>
            <div id="login__toRegister">
                <p>¿Aún no tienes cuenta con nosotros?</p>
                <a href="SignUp.aspx">Regístrate</a>
            </div>
        </div>
    </main>
</asp:Content>
