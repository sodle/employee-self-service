using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace EmployeeSelfService
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("/Login.aspx?nextPage=" + Request.ServerVariables["URL"]);
                return;
            }

            if (!IsPostBack)
            {
                using (var db = new ESSDatabase())
                {
                    var username = Session["Username"].ToString();
                    var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                    var ProfileUser = SelectUser.First();

                    var SelectEmployee = from emp in db.employees where emp.employee_key == ProfileUser.employee_key select emp;
                    var ProfileEmployee = SelectEmployee.First();

                    EmployeeID.Text = ProfileEmployee.employee_key.ToString();
                    FirstName.Text = ProfileEmployee.first_name;
                    LastName.Text = ProfileEmployee.last_name;
                    Email.Text = ProfileEmployee.email;
                    Username.Text = ProfileUser.user_name;
                    StreetAddress1.Text = ProfileEmployee.address_street1;
                    StreetAddress2.Text = ProfileEmployee.address_street2;
                    City.Text = ProfileEmployee.address_city;
                    State.Text = ProfileEmployee.address_state;
                    ZIP.Text = ProfileEmployee.address_zip;
                    Phone.Text = ProfileEmployee.phone;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (FirstName.Text == "" || LastName.Text == "" || StreetAddress1.Text == "" || City.Text == "" || State.Text == "" || ZIP.Text == "" || Phone.Text == "")
            {
                ErrorText.Text = "Please fill out all required fields.";
                return;
            }

            ESSLogin.UpdateEmployee(int.Parse(EmployeeID.Text), FirstName.Text, LastName.Text, StreetAddress1.Text, StreetAddress2.Text, State.Text, City.Text, ZIP.Text, Phone.Text);
        }
    }
}