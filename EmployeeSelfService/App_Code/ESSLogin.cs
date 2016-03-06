using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity;

namespace EmployeeSelfService
{
    public class UserAlreadyExistsException : Exception { }
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
                newUser.employee_key = employee_id;
                newUser.create_date = DateTime.Now;
                newUser.password_hash = HashPassword(username, password);
                db.users.Add(newUser);
                db.SaveChanges();
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
                user.create_date = DateTime.Now;

                db.SaveChanges();
            }
        }
    }
}