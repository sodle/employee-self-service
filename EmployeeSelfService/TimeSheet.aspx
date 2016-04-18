<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TimeSheet.aspx.cs" Inherits="EmployeeSelfService.TimeSheet" %>

<asp:Content ID="Content1Profile" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>

    
<asp:Content ID="Content2Profile" ContentPlaceHolderID="form" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <%--  <head runat="server">
        <title></title>
    </head>--%>
      
    <body>
            
    <%--    <form id="form1" runat="server">--%>
            
            <h1>Time Sheet</h1>
            <hr />
            <div class ="row">
            <div class="col-lg-3 col-md-3">
                <button type="button" class="btn btn-default"><< Previous</button>
            </div>
            <div class="col-lg-3 col-md-3">
                <h4>Week of (Get Week Here) </h4>
            </div>
            <div class="col-lg-3 col-md-3">
                <button type="button" class="btn btn-default">Next >></button>
            </div>
                </div>
            <hr />
            <div class="row">
                <h4>Billable </h4>
                <div class=" col-sm-1 ">
                    Sunday
           
                     <asp:TextBox ID="Sunday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Monday
           
                <asp:TextBox ID="Monday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>
                <div class=" col-sm-1 ">
                    Tuesday
           
                <asp:TextBox ID="Tuesday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Wednesday
           
                <asp:TextBox ID="Wednesday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Thursday
                <asp:TextBox ID="Thursday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Friday
                    <asp:TextBox ID="Friday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox><br />

                </div>
                <div class=" col-sm-1 ">
                    Saturday<br />
                    <asp:TextBox ID="Saturday" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <br />

            </div>
            <div class="row">
                <h4>Non-Billable </h4>
                <div class=" col-sm-1 ">
                    Sunday
           
                     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Monday
           
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>
                <div class=" col-sm-1 ">
                    Tuesday
           
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Wednesday
           
                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Thursday
                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <div class=" col-sm-1 ">
                    Friday
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox><br />

                </div>
                <div class=" col-sm-1 ">
                    Saturday<br />
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control input-sm" Width="50px"></asp:TextBox>
                </div>

                <br />

            </div>
            <div class="row">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="btn btn-primary" /><%-- OnClick="SubmitButton_Click"--%>
                <br />
                <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label>
            </div>


              <%--  <asp:Menu runat="server" CssClass="nav-stacked" >
            <Items runat="server" >
            <asp:MenuItem runat="server" Text="Testing side"></asp:MenuItem>
                 <asp:MenuItem runat="server" Text="Testing side"></asp:MenuItem>  
            </Items>
        </asp:Menu>--%>


      <%--  </form>--%>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script src="scripts/bootstrap.js"></script>
</asp:Content>
