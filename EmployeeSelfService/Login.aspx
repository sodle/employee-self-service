<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EmployeeSelfService.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Log In...</h1>
    <div>
        
        Username&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="UserField" runat="server"></asp:TextBox>
        <br />
        Password&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PassField" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Log In" />
        
        <br />
        <asp:Label ID="ErrorText" runat="server"></asp:Label>
        
    </div>
    </form>
</body>
</html>
