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
        private TextBox[] certTextboxes = new TextBox[15];
        private Button[] certButtons = new Button[15];
        private TextBox[] sTextboxes = new TextBox[15];
        private Button[] sButtons = new Button[15];
        

        protected void Page_Load(object sender, EventArgs e)
        {
            int certCount = Int32.Parse(certificationCount.Text);
            TextBox[] certBoxes = { Certificates, Certificates1, Certificates2, Certificates3, Certificates4,
            Certificates5 };
            certBoxes.CopyTo(certTextboxes, 0);
            Button[] buttons1 = { cancel0, cancel1, cancel2, cancel3, cancel4, cancel5 };
            buttons1.CopyTo(certButtons, 0);

            int sCount = Int32.Parse(skillCount.Text);
            TextBox[] sBoxes = { Skills, Skills1, Skills2, Skills3, Skills4, Skills5 };
            sBoxes.CopyTo(sTextboxes, 0);
            Button[] buttons2 = { sCancel0, sCancel1, sCancel2, sCancel3, sCancel4, sCancel5 };
            buttons2.CopyTo(sButtons, 0);

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
                        if (count == 0)
                        {
                            certBoxes[count].Text = certs[count];
                        }
                        else
                        {
                            certBoxes[count].Text = certs[count];
                            certBoxes[count].Visible = true;
                        }
                        certCount++;
                        count++;
                    }
                    certificationCount.Text = certCount.ToString();
                    count = 0;
                    string[] ski = new string[ProfileEmployee.skills.Count];
                    ski = ProfileEmployee.skills.Select(s => s.skill_text).ToArray();
                    while (count < ProfileEmployee.skills.Count)
                    {
                        if (count == 0)
                        {
                            sBoxes[count].Text = ski[count];
                        }
                        else
                        {
                            sBoxes[count].Text = ski[count];
                            sBoxes[count].Visible = true;
                        }
                        sCount++;
                        count++;
                    }
                    skillCount.Text = sCount.ToString();
                    
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
            int certCount = Int32.Parse(certificationCount.Text);
            int sCount = Int32.Parse(skillCount.Text);

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
                    if(!certTextboxes[certCount].Text.Equals("add a new certificate"))
                    {
                        ESSLogin.CreateCert(ProfileEmployee.employee_key, certTextboxes[certCount].Text);
                        certCount++;
                    }
                    else
                    {
                        certTextboxes[certCount].Visible = false;
                    }

                    int updater = 0;
                    while(updater < ProfileEmployee.certifications.Count)
                    {
                        ESSLogin.UpdateCert(ProfileEmployee.employee_key, 
                            ProfileEmployee.certifications.ElementAt(updater).cert_line_id,
                            certTextboxes[updater].Text);
                        updater++;
                    }

                    int makeRead = 0;
                    while(makeRead < certCount || makeRead == 0)
                    {
                        certButtons[makeRead].Visible = false;
                        certTextboxes[makeRead].ReadOnly = true;
                        certTextboxes[makeRead].BackColor = System.Drawing.Color.White;
                        makeRead++;
                    }
                    certificationCount.Text = certCount.ToString();
                }
                else if (Skills.ReadOnly == false)
                {
                    if (!sTextboxes[sCount].Text.Equals("add a new skill"))
                    {
                        ESSLogin.CreateSkill(ProfileEmployee.employee_key, sTextboxes[sCount].Text);
                        sCount++;
                    }
                    else
                    {
                        sTextboxes[sCount].Visible = false;
                    }

                    int updater = 0;
                    while (updater < ProfileEmployee.skills.Count)
                    {
                        ESSLogin.UpdateSkill(ProfileEmployee.employee_key,
                            ProfileEmployee.skills.ElementAt(updater).skill_line_id,
                            sTextboxes[updater].Text);
                        updater++;
                    }

                    int makeRead = 0;
                    while (makeRead < sCount || makeRead == 0)
                    {
                        sButtons[makeRead].Visible = false;
                        sTextboxes[makeRead].ReadOnly = true;
                        sTextboxes[makeRead].BackColor = System.Drawing.Color.White;
                        makeRead++;
                    }
                    skillCount.Text = sCount.ToString();
                }
            }
        }
        protected void EditCertificates(object sender, EventArgs e)
        {
            int certCount = Int32.Parse(certificationCount.Text);
            using (var db = new ESSDatabase())
            {
                var username = Session["Username"].ToString();
                var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                var ProfileUser = SelectUser.First();

                var SelectEmployee = from emp in db.employees
                                     where emp.employee_key == ProfileUser.employee_key
                                     select emp;
                var ProfileEmployee = SelectEmployee.First();
                int makeRead = 0;
                
                while (makeRead < certCount || makeRead == 0)
                {
                    certButtons[makeRead].Visible = true;
                    certTextboxes[makeRead].ReadOnly = false;
                    certTextboxes[makeRead].BackColor = System.Drawing.Color.LightGray;
                    makeRead++;
                }
                UpdateButton.Visible = true;
                
                if (ProfileEmployee.certifications.Count > 0)
                {
                    certTextboxes[certCount].Visible = true;
                    certTextboxes[certCount].BackColor = System.Drawing.Color.LightGray;
                    certTextboxes[certCount].Text = "add a new certificate";
                }
            }
        }
        protected void EditSkills(object sender, EventArgs e)
        {
            int sCount = Int32.Parse(skillCount.Text);
            using (var db = new ESSDatabase())
            {
                var username = Session["Username"].ToString();
                var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                var ProfileUser = SelectUser.First();

                var SelectEmployee = from emp in db.employees
                                     where emp.employee_key == ProfileUser.employee_key
                                     select emp;
                var ProfileEmployee = SelectEmployee.First();
                int makeRead = 0;

                while (makeRead < sCount || makeRead == 0)
                {
                    sButtons[makeRead].Visible = true;
                    sTextboxes[makeRead].ReadOnly = false;
                    sTextboxes[makeRead].BackColor = System.Drawing.Color.LightGray;
                    makeRead++;
                }
                UpdateButton.Visible = true;

                if (ProfileEmployee.skills.Count > 0)
                {
                    sTextboxes[sCount].Visible = true;
                    sTextboxes[sCount].BackColor = System.Drawing.Color.LightGray;
                    sTextboxes[sCount].Text = "add a new skill";
                }
            }
        }

        protected void DeleteCertificate(object sender, EventArgs e)
        {
            int certCount = Int32.Parse(certificationCount.Text);
            using (var db = new ESSDatabase())
            {
                var username = Session["Username"].ToString();
                var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                var ProfileUser = SelectUser.First();

                var SelectEmployee = from emp in db.employees
                                     where emp.employee_key == ProfileUser.employee_key
                                     select emp;
                var ProfileEmployee = SelectEmployee.First();

                Button pass = (Button)sender;

                string deletingCert = getTextBox(pass).Text;

                int i = 0;
                while(i < ProfileEmployee.certifications.Count)
                {
                    if(deletingCert.Equals(ProfileEmployee.certifications.ElementAt(i).cert_text))
                    {
                        ESSLogin.DeleteCert(ProfileEmployee.employee_key, ProfileEmployee.certifications.ElementAt(i).cert_line_id);
                        certCount--;
                    }
                    i++;
                } 
            }
            certificationCount.Text = certCount.ToString();
            Response.Redirect(Request.RawUrl);
        }

        protected void DeleteSkill(object sender, EventArgs e)
        {
            int sCount = Int32.Parse(skillCount.Text);
            using (var db = new ESSDatabase())
            {
                var username = Session["Username"].ToString();
                var SelectUser = from u in db.users where u.user_name.Equals(username) select u;
                var ProfileUser = SelectUser.First();

                var SelectEmployee = from emp in db.employees
                                     where emp.employee_key == ProfileUser.employee_key
                                     select emp;
                var ProfileEmployee = SelectEmployee.First();

                Button pass = (Button)sender;

                string deletingSkill = getTextBox(pass).Text;

                int i = 0;
                while (i < ProfileEmployee.skills.Count)
                {
                    if (deletingSkill.Equals(ProfileEmployee.skills.ElementAt(i).skill_text))
                    {
                        ESSLogin.DeleteSkill(ProfileEmployee.employee_key, ProfileEmployee.skills.ElementAt(i).skill_line_id);
                        sCount--;
                    }
                    i++;
                }
            }
            skillCount.Text = sCount.ToString();
            Response.Redirect(Request.RawUrl);
        }

        protected TextBox getTextBox(Button iD)
        {
            if (iD.ID == "cancel0")
            {
                return Certificates;
            }
            else if (iD.ID == "cancel1")
            {
                return Certificates1;
            }
            else if (iD.ID == "cancel2")
            {
                return Certificates2;
            }
            else if (iD.ID == "cancel3")
            {
                return Certificates3;
            }
            else if (iD.ID == "cancel4")
            {
                return Certificates4;
            }
            else if (iD.ID == "cancel5")
            {
                return Certificates5;
            }
            if (iD.ID == "sCancel0")
            {
                return Skills;
            }
            else if (iD.ID == "sCancel1")
            {
                return Skills1;
            }
            else if (iD.ID == "sCancel2")
            {
                return Skills2;
            }
            else if (iD.ID == "sCancel3")
            {
                return Skills3;
            }
            else if (iD.ID == "sCancel4")
            {
                return Skills4;
            }
            else if (iD.ID == "sCancel5")
            {
                return Skills5;
            }
            else
                return null;
        }
    }
}