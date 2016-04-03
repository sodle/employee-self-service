<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="EmployeeSelfService.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Change Password</h1>
        <div>
            Current Password:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="CurrentPass" runat="server" TextMode="Password"></asp:TextBox><br />
            New Password:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="NewPass" runat="server" TextMode="Password"></asp:TextBox><br />
            Confirm Password:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="ConfPass" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Button ID="SubmitButton" runat="server" Text="Change Password" OnClick="SubmitButton_Click" /><br />
            <asp:Label ID="ErrorText" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
