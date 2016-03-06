<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EmployeeSelfService.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Register for Employee Self Service</h1>
    <div>

        Employee ID&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="EmployeeID" runat="server"></asp:TextBox>
        <br />
        Choose a username&nbsp;&nbsp;&nbsp; <asp:TextBox ID="Username" runat="server"></asp:TextBox>
        <br />
        Password&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        Confirm password&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Register" />
        <br />
        <asp:Label ID="ErrorText" runat="server"></asp:Label>

    </div>
    </form>
</body>
</html>
