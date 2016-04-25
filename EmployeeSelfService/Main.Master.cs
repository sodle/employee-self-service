using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                li_login.Visible = true;
                li_register.Visible = true;
                li_profile.Visible = false;
                li_skill.Visible = false;
                li_timesheet.Visible = false;
                li_changePassword.Visible = false;
                li_logout.Visible = false;
            }
            else
            {
                li_login.Visible = false;
                li_register.Visible = false;
                li_profile.Visible = true;
                li_skill.Visible = true;
                li_timesheet.Visible = true;
                li_changePassword.Visible = true;
                li_logout.Visible = true;
            }

        }
    }
}