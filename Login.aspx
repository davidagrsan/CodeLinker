<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CodeLinker.Login" %>

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
                <asp:Label ID="lblConnected" runat="server" Text=""></asp:Label>
                <br /><br />
                <label for="loginName"><b>Usuario o Correo</b></label><br />
                <asp:TextBox ID="txtBoxUser" runat="server" placeholder="Usuario o Correo"></asp:TextBox>
                <br><br>
                <label for="pwd"><b>Password</b></label><br>
                <asp:TextBox ID="txtBoxPwd" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                <br><br>
                <input type="checkbox" name="forgotPassword" id="forgotPassword">Did you forget your password?
                <br><br>
                <asp:Button ID="btnLogin" runat="server" Text="Inicia sesión" OnClick="btnLogin_Click" />
                <br><br>
                <p>¿You don't have an account? <a href="">Register</a></p>
            </div>
        </div>
    </main>

</asp:Content>
