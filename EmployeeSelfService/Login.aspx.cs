using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class Login : System.Web.UI.Page
    {
        string DefaultNextPage = "/SessionTest.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session.Abandon();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (UserField.Text == "")
            {
                ErrorText.Text = "Please enter your username.";
                return;
            }
            if (PassField.Text == "")
            {
                ErrorText.Text = "Please enter your password.";
                return;
            }

            try
            {
                Response.BufferOutput = true;
                var userLoggedIn = ESSLogin.TryLogin(UserField.Text, PassField.Text);
                Session["username"] = userLoggedIn.user_name;
                ErrorText.Text = "Logged in successfully!";
                if (Request.QueryString.Get("nextPage") != null)
                    Response.Redirect(Request.QueryString.Get("nextPage"));
                else
                    Response.Redirect(DefaultNextPage);
            }
            catch (InvalidLoginException)
            {
                ErrorText.Text = "Your username and password do not match any of our records.";
                return;
            }
        }
    }
}