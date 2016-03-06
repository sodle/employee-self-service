using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session.Abandon();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Username.Text == "" || EmployeeID.Text == "" || Password.Text == "" || PasswordConfirm.Text == "")
            {
                ErrorText.Text = "Please fill out all required fields.";
                return;
            }
            if (Password.Text != PasswordConfirm.Text)
            {
                ErrorText.Text = "Passwords don't match.";
                return;
            }
            try
            {
                ESSLogin.CreateUser(Username.Text, Password.Text, int.Parse(EmployeeID.Text));
                ErrorText.Text = "User created successfully.";
                Session["Username"] = Username.Text;
                Response.BufferOutput = true;
                Response.Redirect("/SessionTest.aspx");
            }
            catch (UserAlreadyExistsException)
            {
                ErrorText.Text = "A user with that username already exists.";
                return;
            }
            catch (FormatException)
            {
                ErrorText.Text = "Employee ID must be a number.";
                return;
            }
        }
    }
}