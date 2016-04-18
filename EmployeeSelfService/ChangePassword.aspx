<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="EmployeeSelfService.ChangePassword" %>

<asp:Content ID="Content1Profile" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2Profile" ContentPlaceHolderID="form" runat="server">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<body>
        <h1>Change Password</h1>
        <hr />
        <div>
            Current Password:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="CurrentPass" runat="server" CssClass="form-control input-sm" TextMode="Password" Width="250px"></asp:TextBox>
            <br />
           
             New Password:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="NewPass" runat="server" CssClass="form-control input-sm" TextMode="Password" Width="250px"></asp:TextBox>
            <br />
            
            Confirm Password:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="ConfPass" runat="server" CssClass="form-control input-sm" TextMode="Password" Width="250px"></asp:TextBox>
            <br />
            
            <asp:Button ID="SubmitButton" runat="server" Text="Change Password" OnClick="SubmitButton_Click" CssClass="btn btn-primary" /><br />
            <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label>
        </div>
</body>
</html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script src="scripts/bootstrap.js"></script>
</asp:Content>
