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
        
        public static void ChangePassword(string username, string oldPassword, string newPassword)
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
            }
        }
        
        public static void CreateEmployee(string fName, string lName, string address1, string address2, string state, string city, int zip, int phone, string email)
        {
            using (var db = new ESSDatabase())
            {
                var checkEmployeeExists = from e in db.employee where e.email.Equals(email) select e;
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
                
                db.SaveChanges();
            }
        }

        
        public static void CreateUser(string username, string password, int employee_id)
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
            }
        }
                
        public static void CreateCert(int userID, String certDescription)
        {
            using (var db = new ESSDatabase())
            {
                var checkCertExists = from c in db.certification where c.employee_key.Equals(userID) and c.cert_text.Equals(certDescription) select c;
                var certExists = (checkCertExists.Count() != 0);

                if (certExists)
                    throw new CertAlreadyExistsException();

                var newCert = new certification();
                newCert.employee_key = userID;
                newCert.cert_text = certDescription;
                
                db.certification.Add(newCert);
                db.SaveChanges();
            }
        }

        public static void CreateSkill(int userID, String skillDescription)
        {
            using (var db = new ESSDatabase())
            {
                var checkSkillExists = from s in db.skill where s.employee_key.Equals(userID) and s.skill_text.Equals(skillDescription) select s;
                var skillExists = (checkSkillExists.Count() != 0);

                if (skillExists)
                    throw new SkillAlreadyExistsException();

                var newSkill = new skill();
                newSkill.employee_key = userID;
                newSkill.skill_text = skillDescription;

                db.skill.Add(newSkill);
                db.SaveChanges();
            }
        }
        
        public static void CreateTimeReport(int userID, string date, double numHours, bool isBillable)
        {
            using (var db = new ESSDatabase())
            {
                var checkTRExists = from t in db.time_report where t.employee_key.Equals(userID) and t.time_report_date.Equals(date) select t;
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
            }
        }
        
        public static void UpdateEmployee(int userID, string fName, string lName, string address1, string address2, string state, string city, int zip, int phone)
        {
            using (var db = new ESSDatabase())
            {
                var getEmployee = from e in db.employee where e.employee_key.Equals(userID) select e;
                var employee = getEmployee.First();
                
                if(fName)
                    employee.first_name = fName;
                if(lName)
                    employee.last_name = lName;
                if(address1)
                    employee.address_street1 = address1;
                if(address2)
                    employee.address_street2 = address2;
                if(state)
                    employee.address_state = state;
                if(city)
                    employee.address_city = city;
                if(zip != 0)
                    employee.address_zip = zip;
                if(phone != 0)
                    employee.phone = phone;
                
                // insert code to update the employee variable in the database
                db.SaveChanges();
            }
        }
        
        public static void UpdateCert(int userID, int certID, String certDescription)
        {
            using (var db = new ESSDatabase())
            {
                var getCert = from c in db.certification where c.employee_key.Equals(userID) and c.cert_line_id.Equals(certID) select c;
                var cert = getCert.First();

               if(certDescription)
                    cert.cert_text = certDescription;
                    
                //insert code to update certification variable in the database
                db.SaveChanges();
            }
        }
        
        public static void UpdateSkill(int userID, int skillID, String skillDescription)
        {
            using (var db = new ESSDatabase())
            {
                var getSkill = from s in db.skill where s.employee_key.Equals(userID) and s.skill_line_id.Equals(skillID) select s;
                var skills = getSkill.First();

               if(skillDescription)
                    skills.skill_text = skillDescription;
                    
                //insert code to update skill variable in the database
                db.SaveChanges();
            }
        }
        
        public static void UpdateTimeReport(int userID, string date, double numHours, bool isBillable)
        {
            using (var db = new ESSDatabase())
            {
                var getTR = from t in db.time_report where t.employee_key.Equals(userID) select t;
                var timeReport = getTR.First();
                
                if(date)
                    timeReport.time_report_date = date;
                if(numHours)
                    timeReport.time_report_num_hours = numHours;
                timeReport.time_report_billable = isBillable;

                //insert code to update timeReport variable in the database
                db.SaveChanges();
            }
        }
        
        public static void DeleteSkill(int userID, int skillID)
        {
            using (var db = new ESSDatabase())
            {
                var getSkill = from s in db.skill where s.employee_key.Equals(userID) and s.skill_line_id.Equals(skillID) select s;
                var skill = getSkill.First();
                
                db.DeleteObject(skill);
                db.SaveChanges();
            }
        }
        
        public static void DeleteCert()
        {
            using (var db = new ESSDatabase())
            {
                var getCert = from c in db.certification where c.employee_key.Equals(userID) and c.cert_line_id.Equals(certID) select c;
                var cert = getCert.First();
                
                db.DeleteObject(cert);
                db.SaveChanges();
            }
        }
    }
}
