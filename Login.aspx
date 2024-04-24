﻿<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CodeLinker.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/login.css"/>
    <main class="login">
        <div>
            <div>
                <br />
                <img src="Content/img/header-logo.png" alt="logo CodeLinker">
                <br />
            </div>
            <div>
                <br /><br />
                <asp:Label ID="lblConnectedLogIn" runat="server"></asp:Label>
                <br /><br />
                <label for="loginName"><b>Usuario o Correo</b></label><br />
                <asp:TextBox ID="txtBoxUserLogIn" runat="server" placeholder="Usuario o Correo"></asp:TextBox>
                <br><br>
                <label for="pwd"><b>Contraseña</b></label><br>
                <asp:TextBox ID="txtBoxPwdLogIn" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                <br><br>
                <input type="checkbox" name="forgotPassword" id="forgotPassword">Has olvidado tu contraseña?
                <br><br>
                <asp:Button ID="btn__login" runat="server" Text="Crear sesión" OnClick="btnLogin_Click" />
                <br><br>
                <p>¿Aún no tienes una cuenta? <a href="SignUp.aspx">Register</a></p>
            </div>
    </div>
    </main>

</asp:Content>
