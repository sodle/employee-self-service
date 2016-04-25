using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("/Login.aspx?nextPage=" + Request.ServerVariables["URL"]);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (NewPass.Text != ConfPass.Text)
            {
                ErrorText.Text = "New passwords don't match!";
                return;
            }

            try
            {
                ESSLogin.ChangePassword(Session["Username"].ToString(), CurrentPass.Text, NewPass.Text);
                Response.BufferOutput = true;
                Response.Redirect("/Profile.aspx");
            } catch (InvalidLoginException)
            {
                ErrorText.Text = "Your password does not match any of our records.";
                return;
            }
        }
    }
}