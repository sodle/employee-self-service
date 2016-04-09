using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeSelfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Transactions;

namespace EmployeeSelfService.Tests
{
    [TestClass()]
    public class LoginTests
    {
        // The Initialize and Cleanup methods run prior to each of our tests and create a transaction which is later rolled back.
        // This prevents tainting the database with sample data and creating conflicts between tests.

        TransactionScope _txn;

        [TestInitialize]
        public void Initialize()
        {
            _txn = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _txn.Dispose();
        }

        [TestMethod]
        public void TestHashPassword()
        {
            ESSLogin.HashPassword("TestUser", "TestPassword");
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestHashPasswordEmptySalt()
        {
            ESSLogin.HashPassword("", "TestPassword");
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestHashPasswordEmptyPassword()
        {
            ESSLogin.HashPassword("TestUser", "");
        }

        [TestMethod]
        public void TestCreateEmployee()
        {
            ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
        }

        [TestMethod]
        public void TestCreateEmployeeWithoutAddress2()
        {
            ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "", "NY", "New York", "11111", "1234567890", "test@test.com");
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestCreateEmployeeWithoutFirstName()
        {
            ESSLogin.CreateEmployee("", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
        }

        [TestMethod]
        [ExpectedException(typeof(EmployeeAlreadyExistsException))]
        public void TestCreateEmployeeWithDuplicateEmail()
        {
            ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateEmployee("Imposter", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
        }

        [TestMethod]
        public void TestCreateUser()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestCreateUserWithoutPassword()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "", EmployeeKey);
        }

        [TestMethod]
        [ExpectedException(typeof(EmployeeDoesNotExistException))]
        public void TestCreateUserWithNonExistentEmployee()
        {
            ESSLogin.CreateUser("testuser", "testpass", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void TestCreateUserWithDuplicateUsername()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
        }

        [TestMethod]
        public void TestChangePassword()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
            ESSLogin.ChangePassword("testuser", "testpass", "newtestpass");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidLoginException))]
        public void TestChangePasswordForNonExistentUser()
        {
            ESSLogin.ChangePassword("testuser", "testpass", "newtestpass");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidLoginException))]
        public void TestChangePasswordWithWrongOldPassword()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
            ESSLogin.ChangePassword("testuser", "wrongpass", "newtestpass");
        }

        [TestMethod]
        public void TestLogin()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
            var LoggedInUser = ESSLogin.TryLogin("testuser", "testpass");
            Assert.AreEqual(LoggedInUser.user_name, "testuser");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidLoginException))]
        public void TestLoginWithNonExistentUserName()
        {
            var LoggedInUser = ESSLogin.TryLogin("testuser", "testpass");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidLoginException))]
        public void TestLoginWithWrongPassword()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateUser("testuser", "testpass", EmployeeKey);
            var LoggedInUser = ESSLogin.TryLogin("testuser", "wrongpass");
        }
    }
}