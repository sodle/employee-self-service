using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity;

namespace EmployeeSelfService
{
    public class EmployeeAlreadyExistsException : Exception { }
    public class UserAlreadyExistsException : Exception { }
    public class CertAlreadyExistsException : Exception { }
    public class SkillAlreadyExistsException : Exception { }
    public class TimeReportAlreadyExistsException : Exception { }
    public class EmployeeDoesNotExistException : Exception { }
    public class UserDoesNotExistException : Exception { }
    public class CertDoesNotExistException : Exception { }
    public class SkillDoesNotExistException : Exception { }
    public class TimeReportDoesNotExistException : Exception { }
    public class InvalidLoginException : Exception { }
    public class RequiredValueMissingException : Exception { }

    public class ESSLogin
    {
        public static string HashPassword(string username, string password)
        {
            SHA256 hasher = SHA256.Create();
            string salted = username + password;
            byte[] saltyBytes = Encoding.Default.GetBytes(salted);
            byte[] hashBytes = hasher.ComputeHash(saltyBytes);

            if (username == "" || password == "")
                throw new RequiredValueMissingException();

            return Encoding.Default.GetString(hashBytes);
        }

        public static user TryLogin(string username, string password)
        {
            using (var db = new ESSDatabase())
            {
                var users = from u in db.users where u.user_name.Equals(username) select u;

                if (users.Count() == 0)
                    throw new InvalidLoginException();

                var user = users.First();

                if (HashPassword(username, password) == user.password_hash)
                    return user;

                throw new InvalidLoginException();
            }
        }
        
        public static int ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var db = new ESSDatabase())
            {
                var users = from u in db.users where u.user_name.Equals(username) select u;

                if (users.Count() == 0)
                    throw new InvalidLoginException();

                var user = users.First();

                if (HashPassword(username, oldPassword) != user.password_hash)
                    throw new InvalidLoginException();

                user.password_hash = HashPassword(username, newPassword);
                user.create_date = DateTime.Now;
                db.SaveChanges();

                return user.user_key;
            }
        }
        
        public static int CreateEmployee(string fName, string lName, string address1, string address2, string state, string city, string zip, string phone, string email)
        {
            using (var db = new ESSDatabase())
            {
                if (fName == "" || lName == "" || address1 == "" || state == "" || city == "" || zip == "" || phone == "" || email == "")
                    throw new RequiredValueMissingException();

                var checkEmployeeExists = from e in db.employees where e.email.Equals(email) select e;
                var employeeExists = (checkEmployeeExists.Count() != 0);

                if (employeeExists)
                    throw new EmployeeAlreadyExistsException();

                var newEmployee = new employee();
                newEmployee.first_name = fName;
                newEmployee.last_name = lName;
                newEmployee.address_street1 = address1;
                newEmployee.address_street2 = address2;
                newEmployee.address_state = state;
                newEmployee.address_city = city;
                newEmployee.address_zip = zip;
                newEmployee.phone = phone;
                newEmployee.email = email;
                newEmployee.create_date = DateTime.Now;

                db.employees.Add(newEmployee);
                db.SaveChanges();

                return newEmployee.employee_key;
            }
        }

        
        public static int CreateUser(string username, string password, int employee_id)
        {
            using (var db = new ESSDatabase())
            {
                if (username == "" || password == "")
                    throw new RequiredValueMissingException();

                var checkEmployeeExists = from e in db.employees where e.employee_key.Equals(employee_id) select e;
                var employeeExists = (checkEmployeeExists.Count() != 0);

                if (!employeeExists)
                    throw new EmployeeDoesNotExistException();

                var checkUserExists = from u in db.users where u.user_name.Equals(username) select u;
                var userExists = (checkUserExists.Count() != 0);

                if (userExists)
                    throw new UserAlreadyExistsException();

                var newUser = new user();
                newUser.user_name = username;
                newUser.password_hash = HashPassword(username, password);
                newUser.employee_key = employee_id;
                newUser.create_date = DateTime.Now;
                
                db.users.Add(newUser);
                db.SaveChanges();

                return newUser.user_key;
            }
        }
                
        public static int CreateCert(int userID, String certDescription)
        {
            using (var db = new ESSDatabase())
            {
                var checkCertExists = from c in db.certifications where c.employee_key.Equals(userID) && c.cert_text.Equals(certDescription) select c;
                var certExists = (checkCertExists.Count() != 0);

                if (certExists)
                    throw new CertAlreadyExistsException();

                if (certDescription == "")
                    throw new RequiredValueMissingException();

                var newCert = new certification();
                newCert.employee_key = userID;
                newCert.cert_text = certDescription;
                newCert.create_date = DateTime.Now;
                
                db.certifications.Add(newCert);
                db.SaveChanges();

                return newCert.cert_line_id;
            }
        }

        public static int CreateSkill(int userID, String skillDescription)
        {
            using (var db = new ESSDatabase())
            {
                var checkSkillExists = from s in db.skills where s.employee_key.Equals(userID) && s.skill_text.Equals(skillDescription) select s;
                var skillExists = (checkSkillExists.Count() != 0);

                if (skillExists)
                    throw new SkillAlreadyExistsException();

                if (skillDescription == "")
                    throw new RequiredValueMissingException();

                var newSkill = new skill();
                newSkill.employee_key = userID;
                newSkill.skill_text = skillDescription;
                newSkill.create_date = DateTime.Now;

                db.skills.Add(newSkill);
                db.SaveChanges();

                return newSkill.skill_line_id;
            }
        }
        
        public static int CreateTimeReport(int userID, DateTime date, decimal numHours, bool isBillable)
        {
            using (var db = new ESSDatabase())
            {
                var checkTRExists = from t in db.time_report where t.employee_key.Equals(userID) && t.time_report_date.Equals(date) select t;
                var trExists = (checkTRExists.Count() != 0);

                if (trExists)
                    throw new TimeReportAlreadyExistsException();

                var newTimeReport = new time_report();
                newTimeReport.employee_key = userID;
                newTimeReport.time_report_date = date;
                newTimeReport.time_report_num_hours = numHours;
                newTimeReport.time_report_billable = isBillable;
                newTimeReport.create_date = DateTime.Now;

                db.time_report.Add(newTimeReport);
                db.SaveChanges();

                return newTimeReport.time_report_id;
            }
        }
        
        public static int UpdateEmployee(int userID, string fName, string lName, string address1, string address2, string state, string city, string zip, string phone)
        {
            using (var db = new ESSDatabase())
            {
                var getEmployee = from e in db.employees where e.employee_key.Equals(userID) select e;
                var employee = getEmployee.First();

                if (fName == "" || lName == "" || address1 == "" || state == "" || city == "" || zip == "" || phone == "")
                    throw new RequiredValueMissingException();

                employee.first_name = fName;
                employee.last_name = lName;
                employee.address_street1 = address1;
                employee.address_street2 = address2;
                employee.address_state = state;
                employee.address_city = city;
                employee.address_zip = zip;
                employee.phone = phone;

                employee.create_date = DateTime.Now;
                
                // insert code to update the employee variable in the database
                db.SaveChanges();

                return employee.employee_key;
            }
        }
        
        public static int UpdateCert(int userID, int certID, String certDescription)
        {
            using (var db = new ESSDatabase())
            {
                var getCert = from c in db.certifications where c.employee_key.Equals(userID) && c.cert_line_id.Equals(certID) select c;
                var cert = getCert.First();

                var checkDup = from c in db.certifications where c.employee_key.Equals(userID) && c.cert_text.Equals(certDescription) && !c.cert_line_id.Equals(cert.cert_line_id) select c;
                if (checkDup.Count() != 0)
                    throw new CertAlreadyExistsException();

                if (certDescription == "")
                    throw new RequiredValueMissingException();

                cert.cert_text = certDescription;

                cert.create_date = DateTime.Now;
                    
                //insert code to update certification variable in the database
                db.SaveChanges();
                return cert.cert_line_id;
            }
        }
        
        public static int UpdateSkill(int userID, int skillID, String skillDescription)
        {
            using (var db = new ESSDatabase())
            {
                var getSkill = from s in db.skills where s.employee_key.Equals(userID) && s.skill_line_id.Equals(skillID) select s;
                var skills = getSkill.First();

                var checkDup = from s in db.skills where s.employee_key.Equals(userID) && s.skill_text.Equals(skillDescription) && !s.skill_line_id.Equals(skills.skill_line_id) select s;
                if (checkDup.Count() != 0)
                    throw new SkillAlreadyExistsException();

                if (skillDescription == "")
                    throw new RequiredValueMissingException();

                skills.skill_text = skillDescription;

                skills.create_date = DateTime.Now;
                    
                //insert code to update skill variable in the database
                db.SaveChanges();
                return skills.skill_line_id;
            }
        }
        
        public static int UpdateTimeReport(int userID, int timeReportID, DateTime date, decimal numHours, bool isBillable)
        {
            using (var db = new ESSDatabase())
            {
                var getTR = from t in db.time_report where t.time_report_id.Equals(timeReportID) select t;
                if (getTR.Count() == 0)
                    throw new TimeReportDoesNotExistException();

                var timeReport = getTR.First();

                var checkTRExists = from t in db.time_report where t.employee_key.Equals(userID) && t.time_report_date.Equals(date) && !t.time_report_id.Equals(timeReport.time_report_id) select t;
                var trExists = (checkTRExists.Count() != 0);

                if (trExists)
                    throw new TimeReportAlreadyExistsException();

                timeReport.time_report_date = date;
                timeReport.time_report_num_hours = numHours;
                timeReport.time_report_billable = isBillable;

                timeReport.create_date = DateTime.Now;

                //insert code to update timeReport variable in the database
                db.SaveChanges();
                return timeReport.time_report_id;
            }
        }
        
        public static void DeleteSkill(int userID, int skillID)
        {
            using (var db = new ESSDatabase())
            {
                var getSkill = from s in db.skills where s.employee_key.Equals(userID) && s.skill_line_id.Equals(skillID) select s;
                if (getSkill.Count() == 0)
                    throw new SkillDoesNotExistException();

                var skill = getSkill.First();
                
                db.skills.Remove(skill);
                db.SaveChanges();
            }
        }
        
        public static void DeleteCert(int userID, int certID)
        {
            using (var db = new ESSDatabase())
            {
                var getCert = from c in db.certifications where c.employee_key.Equals(userID) && c.cert_line_id.Equals(certID) select c;
                if (getCert.Count() == 0)
                    throw new CertDoesNotExistException();

                var cert = getCert.First();
                
                db.certifications.Remove(cert);
                db.SaveChanges();
            }
        }
    }
}
