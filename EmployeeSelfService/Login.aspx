<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EmployeeSelfService.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="form" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
       <%-- <form id="form1" runat="server">--%>
            <div class="row">
                <div class="span7 center" style="background-color=">
            <h1>Log In</h1>
                    
            <div>
                <div>
                    Username&nbsp;&nbsp;<asp:TextBox ID="UserField" runat="server"></asp:TextBox>
                </div>
                <br />
                <div>
                    Password&nbsp;&nbsp; <asp:TextBox ID="PassField" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <asp:Button ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Log In" />
                <br />

                <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
            </div>
       <%-- </form>--%>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
