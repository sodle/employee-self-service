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
        <div>
        <asp:TextBox ID="certificationCount" runat="server" Text="0" Visible="false"></asp:TextBox>
        <asp:TextBox ID="Certificates" runat="server" Height="40px" Width="500px" CssClass="form-control input-lg" ReadOnly="true" Style="resize:none; float:left" BorderWidth="0px" BackColor="White"></asp:TextBox> 
        <asp:Button ID="cancel0" runat="server" Height="40px" Text="X" OnClick="DeleteCertificate" BackColor="Red" CssClass="btn btn-group-sm" Visible="false" Style="position:relative"/>
        <br />
        </div>
        <div>
        <asp:TextBox ID="Certificates1" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="cancel1" runat="server" Height="40px" Text="X" OnClick="DeleteCertificate" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        </div>
        <div>
        <asp:TextBox ID="Certificates2" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="cancel2" runat="server" Height="40px" Text="X" OnClick="DeleteCertificate" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        </div>
        <asp:TextBox ID="Certificates3" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="cancel3" runat="server" Height="40px" Text="X" OnClick="DeleteCertificate" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        <asp:TextBox ID="Certificates4" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="cancel4" runat="server" Height="40px" Text="X" OnClick="DeleteCertificate" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        <asp:TextBox ID="Certificates5" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="cancel5" runat="server" Height="40px" Text="X" OnClick="DeleteCertificate" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
            <asp:Button ID="EditCert" runat="server" Text="edit" OnClick="EditCertificates" CssClass="btn btn-sm pull-right" />
        <br />
    
    </div>
        <h1>Skills</h1>
        <hr />
        <div>
            <div>
        <asp:TextBox ID="skillCount" runat="server" Text="0" Visible="false"></asp:TextBox>
        <asp:TextBox ID="Skills" runat="server" Height="40px" Width="500px" CssClass="form-control input-lg" ReadOnly="true" Style="resize:none; float:left" BorderWidth="0px" BackColor="White"></asp:TextBox> 
        <asp:Button ID="sCancel0" runat="server" Height="40px" Text="X" OnClick="DeleteSkill" BackColor="Red" CssClass="btn btn-group-sm" Visible="false" Style="position:relative"/>
        <br />
        </div>
        <div>
        <asp:TextBox ID="Skills1" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="sCancel1" runat="server" Height="40px" Text="X" OnClick="DeleteSkill" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        </div>
        <div>
        <asp:TextBox ID="Skills2" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="sCancel2" runat="server" Height="40px" Text="X" OnClick="DeleteSkill" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        </div>
        <asp:TextBox ID="Skills3" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="sCancel3" runat="server" Height="40px" Text="X" OnClick="DeleteSkill" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        <asp:TextBox ID="Skills4" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="sCancel4" runat="server" Height="40px" Text="X" OnClick="DeleteSkill" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
        <asp:TextBox ID="Skills5" runat="server" Height="40px" CssClass="form-control input-lg" Style="resize:none; float:left"
            BorderWidth="0px" BackColor="White" Width="500px" Visible="false"></asp:TextBox>
        <asp:Button ID="sCancel5" runat="server" Height="40px" Text="X" OnClick="DeleteSkill" BackColor="Red" CssClass="btn btn-group-sm" Visible="false"/>
        <br />
            <asp:Button ID="editSkills" runat="server" Text="edit" OnClick="EditSkills" CssClass="btn btn-sm pull-right" />
        <br />

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