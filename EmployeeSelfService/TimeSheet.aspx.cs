using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class TimeSheet : System.Web.UI.Page
    {
        private DateTime currentDay;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("/Login.aspx?nextPage=" + Request.ServerVariables["URL"]);
                return;
            }

            currentDay = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            week_header.InnerText = "Week of " + currentDay.ToShortDateString();
        }

        protected void getPreviousWeek(object sender, EventArgs e)
        {
            prev_count.Text = (Int32.Parse(prev_count.Text) + 1).ToString();
            next_count.Text = (Int32.Parse(next_count.Text) - 1).ToString();
            week_header.InnerText = "Week of " + currentDay.AddDays(-(int)currentDay.DayOfWeek - (7*Int32.Parse(prev_count.Text))).ToShortDateString();
        }
        
        protected void getNextWeek(object sender, EventArgs e)
        {
            prev_count.Text = (Int32.Parse(prev_count.Text) - 1).ToString();
            next_count.Text = (Int32.Parse(next_count.Text) + 1).ToString();
            week_header.InnerText = "Week of " + currentDay.AddDays(-(int)currentDay.DayOfWeek + (7*Int32.Parse(next_count.Text))).ToShortDateString();
        }
    }
}