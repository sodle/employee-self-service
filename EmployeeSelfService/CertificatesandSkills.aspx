<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CertificatesandSkills.aspx.cs" Inherits="EmployeeSelfService.CertificatesandSkills" %>

<asp:Content ID="Content1Profile" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2Profile" ContentPlaceHolderID="form" runat="server">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%-- <head runat="server">
    <title></title>
</head>--%>
<body>
        <h1>Certificates</h1>
        <hr />
    <div>
        <asp:TextBox ID="Certificates" runat="server" Width="500px" CssClass="form-control input-lg" ReadOnly="true" TextMode="MultiLine"
              Style="resize:none" BorderWidth="0px" BackColor="White" Height="150"></asp:TextBox>
        <asp:PlaceHolder ID="AddCert" runat="server"></asp:PlaceHolder>
        <br />
            <asp:Button ID="EditCert" runat="server" Text="edit" OnClick="EditCertificates" CssClass="btn btn-sm pull-right" />
    </div>
        <h1>Skills</h1>
        <hr />
        <div>
            <asp:TextBox ID="Skills" runat="server" CssClass="form-control input-lg" Width="500px" TextMode="MultiLine" ReadOnly="true"
                Style="resize:none" BorderWidth="0px" BackColor="White" Height ="150"></asp:TextBox>
        <br />
            <asp:Button ID="AddSkill" runat="server" Text="edit" OnClick="EditSkills" CssClass="btn btn-sm pull-right" />

            <asp:Button ID="UpdateButton" runat="server" Text="Update" OnClick="SubmitButton_Click" CssClass="btn btn-primary" Visible="false" />
                <br />
                <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label>
        </div>
</body>
</html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script src="scripts/bootstrap.js"></script>
</asp:Content>