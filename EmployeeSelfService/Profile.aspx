<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="EmployeeSelfService.Profile" %>

<asp:Content ID="Content1Profile" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2Profile" ContentPlaceHolderID="form" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
  <%--  <head runat="server">
        <title></title>
    </head>--%>
    <body>
    
      <%--  <form id="form1" runat="server">--%>
            
            <h1>Your Profile</h1>
            <hr />
            <div>
                Employee ID&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="EmployeeID" runat="server" Enabled="false" TextMode="Number" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                First name&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="FirstName" runat="server" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                Last name&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="LastName" runat="server" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                Email address&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="Email" runat="server" TextMode="Email" Enabled="False" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                Username&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="Username" runat="server" Enabled="False" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                Street address<br />
                <asp:TextBox ID="StreetAddress1" runat="server" CssClass="form-control input-sm" Width="250px"></asp:TextBox><br />
                <asp:TextBox ID="StreetAddress2" runat="server" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                City&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="City" runat="server" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                State&nbsp;&nbsp;&nbsp;
           
                <asp:DropDownList ID="State" runat="server" CssClass="form-control" Width="250px">
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachussetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">New Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">New Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList>
                <br />
                ZIP&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="ZIP" runat="server" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                Phone Number&nbsp;&nbsp;&nbsp;
           
                <asp:TextBox ID="Phone" runat="server" TextMode="Phone" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                <br />
                <asp:Button ID="SubmitButton" runat="server" Text="Update" OnClick="SubmitButton_Click" CssClass="btn btn-primary" />
                <br />
                <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label>
            </div>
       <%-- </form>--%>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script src="scripts/bootstrap.js"></script>
</asp:Content>
