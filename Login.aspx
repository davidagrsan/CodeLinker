<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CodeLinker._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/login.css"/>
    <main class="home">
        <div>
        <form action="" method="post" class="modal_content">
            <div>
                <br>
                <img src="Content/img/header-logo.png" alt="logo CodeLinker">
                <br><br>
            </div>
            <div>
                <label for="loginName"><b>User or Email</b></label><br>
                <input type="text" name="user" id="loginName" placeholder="Username or Email">
                <br><br>
                <label for="pwd"><b>Password</b></label><br>
                <input type="password" name="user" id="loginName" placeholder="Password">
                <br><br>
                <input type="checkbox" name="forgotPassword" id="forgotPassword">Did you forget your password?
                <br><br>
                <button class="login__button">Log In</button>
                <br><br>
                <p>¿You don't have an account? <a href="">Register</a></p>
            </div>
        </form>
    </div>
    </main>

</asp:Content>
