using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class SessionTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
                sessioncheck.InnerText = "No session is active.";
            else
                sessioncheck.InnerText = "Logged in as " + Session["username"] + ".";
        }
    }
}