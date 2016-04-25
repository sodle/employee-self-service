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
            if (FirstName.Text == "" || LastName.Text == "" || Email.Text == "" || Username.Text == "" || Password.Text == "" || PasswordConfirm.Text == "" || StreetAddress1.Text == "" || City.Text == "" || State.Text == "" || ZIP.Text == "" || Phone.Text == "")
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
                int employeeID = ESSLogin.CreateEmployee(FirstName.Text, LastName.Text, StreetAddress1.Text, StreetAddress2.Text, State.Text, City.Text, ZIP.Text, Phone.Text, Email.Text);
                ESSLogin.CreateUser(Username.Text, Password.Text, employeeID);
                ErrorText.Text = "User created successfully.";
                Session["Username"] = Username.Text;
                Response.BufferOutput = true;
                Response.Redirect("/Profile.aspx");
            }
            catch (UserAlreadyExistsException)
            {
                ErrorText.Text = "A user with that username already exists.";
                return;
            }
            catch (EmployeeAlreadyExistsException)
            {
                ErrorText.Text = "An employee with that email address already exists.";
                return;
            }
        }
    }
}