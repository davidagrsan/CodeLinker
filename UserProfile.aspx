<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="CodeLinker.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnChangePhoto" runat="server" Text="Foto" OnClick="btnChangePhoto_Click" />
            <asp:Button ID="btnDisconnect" runat="server" OnClick="btnDisconnect_Click" Text="Desconectar" />
            <br />
            <br />
            <br />
            <br />
            <asp:Image ID="ProfilePicture" runat="server" />
        </div>
    </form>
</body>
</html>
