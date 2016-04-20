<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="EmployeeSelfService.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">

    <style type="text/css">
        .auto-style2 {
            width: 250px;
            height: 47px;
        }

        #button1 {
            background-color: transparent;
            border-color: #ccc;
            color: #ccc;
        }

        #button2 {
            background-color: transparent;
            border-color: #ccc;
            color: #ccc;
        }

        #button1:hover {
            color: #ccc;
            background-color: #FF4411;
        }

        #button2:hover {
            color: #ccc;
            background-color: #FF4411;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="form" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <body>
        <br />
        <br />
        <br />
        <br />
         <br />
        <br />
        <div style="background-image: url('Images/workImageSM2B.jpg'); width: 100%; height: 400px">
            <br />
            <br />
            <br />
            <br />

            <%-- <hr style="color: white" />--%>
            <div class="col-lg-6">
                <h1 style="color: white; font-family: Arial; font-size: 30pt; text-align: center"><b>Employee Self Service</b></h1>

                <h3 style="color: white; font-family: 'Open Sans'; font-weight: 100; font-style: italic; font-size: medium; text-align: center">Sponsored by</h3>


                <img style="margin: 0px auto; display: block" class="auto-style2" src="Images/SogetiIconLarge.jpg" /><br />


                <br />
                <div class="row">
                    <div class="col-xs-8 col-xs-offset-2 col-sm-4 col-sm-offset-4 col-md-4 col-md-offset-4">
                        <a href="/login.aspx" role="button" id="button1" type="button" class="btn btn-secondary-outline">Login</a>
                        &nbsp;&nbsp;&nbsp;
                <a href="/Register.aspx" role="button" id="button2" class="btn btn-secondary-outline">Register</a>
                    </div>
                </div>


            </div>
            <br />
            <br />

        </div>

    </body>
    </html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
