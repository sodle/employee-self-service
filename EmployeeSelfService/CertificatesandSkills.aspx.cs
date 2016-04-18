using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeSelfService
{
    public partial class CertificatesandSkills : System.Web.UI.Page
    {
        private int idCount = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("/Login.aspx?nextPage=" + Request.ServerVariables["URL"]);
                return;
            }

            if(!IsPostBack)
            {
                using (var db = new ESSDatabase())
                {
                    int count = 0;
                    var username = Session["Username"].ToString();
                    var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                    var ProfileUser = SelectUser.First();

                    var SelectEmployee = from emp in db.employees
                                         where emp.employee_key == ProfileUser.employee_key
                                         select emp;
                    var ProfileEmployee = SelectEmployee.First();

                    string[] certs = new string[ProfileEmployee.certifications.Count];
                    certs = ProfileEmployee.certifications.Select(c => c.cert_text).ToArray();
                    while (count < ProfileEmployee.certifications.Count)
                    {

                        Certificates.Text += certs[count]+"\n";
                        count++;
                    }
                    count = 0;
                    string[] ski = new string[ProfileEmployee.skills.Count];
                    ski = ProfileEmployee.skills.Select(s => s.skill_text).ToArray();
                    while (count < ProfileEmployee.skills.Count)
                    {
                        Skills.Text += ski[count] + "\n";
                        count++;
                    }
                    
                }
            }

            if(Certificates.Text.Equals(""))
            {
                Certificates.Text = "No certificates were found in our data.";
            }

            if (Skills.Text.Equals(""))
            {
                Skills.Text = "No skills were found in our data.";
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            UpdateButton.Visible = false;

            using (var db = new ESSDatabase())
            {
                var username = Session["Username"].ToString();
                var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                var ProfileUser = SelectUser.First();

                var SelectEmployee = from emp in db.employees
                                     where emp.employee_key == ProfileUser.employee_key
                                     select emp;
                var ProfileEmployee = SelectEmployee.First();
                if (Certificates.ReadOnly == false)
                {
                    if (ProfileEmployee.certifications.Count == 0)
                    {
                        ESSLogin.CreateCert(ProfileEmployee.employee_key, Certificates.Text);
                    }
                    else
                    {
                        ESSLogin.UpdateCert(ProfileUser.employee_key, ProfileEmployee.certifications.ElementAt(0).cert_line_id, Certificates.Text);
                    }
                    Certificates.ReadOnly = true;
                    Certificates.BackColor = System.Drawing.Color.White;
                }
                else if (Skills.ReadOnly == false)
                {
                    if (ProfileEmployee.skills.Count == 0)
                    {
                        ESSLogin.CreateSkill(ProfileEmployee.employee_key, Skills.Text);
                    }
                    else {
                        ESSLogin.UpdateSkill(ProfileEmployee.employee_key, ProfileEmployee.skills.ElementAt(0).skill_line_id, Skills.Text);
                    }
                    Skills.ReadOnly = true;
                    Skills.BackColor = System.Drawing.Color.White;
                }
            }
        }
        protected void EditCertificates(object sender, EventArgs e)
        {
            Certificates.ReadOnly = false;
            Certificates.BackColor = System.Drawing.Color.LightGray;
            UpdateButton.Visible = true;
        }
        protected void EditSkills(object sender, EventArgs e)
        {
            Skills.ReadOnly = false;
            Skills.BackColor = System.Drawing.Color.LightGray;
            UpdateButton.Visible = true;
        }
    }
}