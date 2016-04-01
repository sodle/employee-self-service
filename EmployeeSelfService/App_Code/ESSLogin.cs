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

    public class ESSLogin
    {
        public static string HashPassword(string username, string password)
        {
            SHA256 hasher = SHA256.Create();
            string salted = username + password;
            byte[] saltyBytes = Encoding.Default.GetBytes(salted);
            byte[] hashBytes = hasher.ComputeHash(saltyBytes);
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
                db.SaveChanges();

                return user.user_key;
            }
        }
        
        public static int CreateEmployee(string fName, string lName, string address1, string address2, string state, string city, string zip, string phone, string email)
        {
            using (var db = new ESSDatabase())
            {
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

                db.employees.Add(newEmployee);
                db.SaveChanges();

                return newEmployee.employee_key;
            }
        }

        
        public static int CreateUser(string username, string password, int employee_id)
        {
            using (var db = new ESSDatabase())
            {
                var checkUserExists = from u in db.users where u.user_name.Equals(username) select u;
                var userExists = (checkUserExists.Count() != 0);

                if (userExists)
                    throw new UserAlreadyExistsException();

                var newUser = new user();
                newUser.user_name = username;
                newUser.password_hash = HashPassword(username, password);
                newUser.employee_key = employee_id;
                
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

                var newCert = new certification();
                newCert.employee_key = userID;
                newCert.cert_text = certDescription;
                
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

                var newSkill = new skill();
                newSkill.employee_key = userID;
                newSkill.skill_text = skillDescription;

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
                
                if (!fName.Equals(null))
                    employee.first_name = fName;
                if (!lName.Equals(null))
                    employee.last_name = lName;
                if (!address1.Equals(null))
                    employee.address_street1 = address1;
                if (!address2.Equals(null))
                    employee.address_street2 = address2;
                if (!state.Equals(null))
                    employee.address_state = state;
                if (!city.Equals(null))
                    employee.address_city = city;
                if (!zip.Equals(null))
                    employee.address_zip = zip;
                if (!phone.Equals(null))
                    employee.phone = phone;
                
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

               if (!certDescription.Equals(null))
                    cert.cert_text = certDescription;
                    
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

               if (!skillDescription.Equals(null))
                    skills.skill_text = skillDescription;
                    
                //insert code to update skill variable in the database
                db.SaveChanges();
                return skills.skill_line_id;
            }
        }
        
        public static int UpdateTimeReport(int userID, DateTime date, decimal numHours, bool isBillable)
        {
            using (var db = new ESSDatabase())
            {
                var getTR = from t in db.time_report where t.employee_key.Equals(userID) select t;
                var timeReport = getTR.First();
                
                if (!date.Equals(null))
                    timeReport.time_report_date = date;
                if (!numHours.Equals(null))
                    timeReport.time_report_num_hours = numHours;
                timeReport.time_report_billable = isBillable;

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
                var cert = getCert.First();
                
                db.certifications.Remove(cert);
                db.SaveChanges();
            }
        }
    }
}
