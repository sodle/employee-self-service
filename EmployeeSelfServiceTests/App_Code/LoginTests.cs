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
        // This prevents conflicts between tests.

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

        [TestMethod]
        public void TestCreateCert()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestCreateCertWithEmptyDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateCert(EmployeeKey, "");
        }

        [TestMethod]
        [ExpectedException(typeof(CertAlreadyExistsException))]
        public void TestCreateCertWithDuplicateDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
            ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
        }

        [TestMethod]
        public void TestCreateSkill()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestCreateSkillWithEmptyDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateSkill(EmployeeKey, "");
        }

        [TestMethod]
        [ExpectedException(typeof(SkillAlreadyExistsException))]
        public void TestCreateSkillWithDuplicateDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
            ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
        }

        [TestMethod]
        public void TestCreateTimeReport()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateTimeReport(EmployeeKey, DateTime.Now.Date, 10, true);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeReportAlreadyExistsException))]
        public void TestCreateTimeReportWithDuplicateDate()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.CreateTimeReport(EmployeeKey, DateTime.Now.Date, 10, true);
            ESSLogin.CreateTimeReport(EmployeeKey, DateTime.Now.Date, 10, true);
        }

        [TestMethod]
        public void TestUpdateEmployee()
        {
            var key = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.UpdateEmployee(key, "First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890");
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredValueMissingException))]
        public void TestUpdateEmployeeWithMissingLastName()
        {
            var key = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.UpdateEmployee(key, "First name", "", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890");
        }

        [TestMethod]
        public void TestUpdateSkill()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var SkillKey = ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
            ESSLogin.UpdateSkill(EmployeeKey, SkillKey, "MSTest");
        }

        [TestMethod]
        public void TestUpdateSkillWithSameDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var SkillKey = ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
            ESSLogin.UpdateSkill(EmployeeKey, SkillKey, "ASP.NET Unit Testing");
        }

        [TestMethod]
        [ExpectedException(typeof(SkillAlreadyExistsException))]
        public void TestUpdateSkillWithDuplicateDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var SkillKey1 = ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
            var SkillKey2 = ESSLogin.CreateSkill(EmployeeKey, "Python Development");
            ESSLogin.UpdateSkill(EmployeeKey, SkillKey2, "ASP.NET Unit Testing");
        }

        [TestMethod]
        public void TestUpdateCert()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var CertKey = ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
            ESSLogin.UpdateCert(EmployeeKey, CertKey, "MSTest");
        }

        [TestMethod]
        public void TestUpdateCertWithSameDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var CertKey = ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
            ESSLogin.UpdateCert(EmployeeKey, CertKey, "ASP.NET Power User");
        }

        [TestMethod]
        [ExpectedException(typeof(CertAlreadyExistsException))]
        public void TestUpdateCertWithDuplicateDescription()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var CertKey1 = ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
            var CertKey2 = ESSLogin.CreateCert(EmployeeKey, "Python Ninja");
            ESSLogin.UpdateCert(EmployeeKey, CertKey2, "ASP.NET Power User");
        }

        [TestMethod]
        public void TestUpdateTimeReport()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var ReportKey = ESSLogin.CreateTimeReport(EmployeeKey, DateTime.Now.Date, 10, true);
            ESSLogin.UpdateTimeReport(EmployeeKey, ReportKey, DateTime.Now.Date, 15, false);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeReportAlreadyExistsException))]
        public void TestUpdateTimeReportWithDuplicateDate()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var ReportKey1 = ESSLogin.CreateTimeReport(EmployeeKey, DateTime.Now.Date, 10, true);
            var ReportKey2 = ESSLogin.CreateTimeReport(EmployeeKey, DateTime.Now.Date.AddDays(-1), 10, true);
            ESSLogin.UpdateTimeReport(EmployeeKey, ReportKey2, DateTime.Now.Date, 15, false);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeReportDoesNotExistException))]
        public void TestUpdateNonExistentTimeReport()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.UpdateTimeReport(EmployeeKey, 0, DateTime.Now.Date, 15, false);
        }

        [TestMethod]
        public void TestDeleteSkill()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var SkillKey = ESSLogin.CreateSkill(EmployeeKey, "ASP.NET Unit Testing");
            ESSLogin.DeleteSkill(EmployeeKey, SkillKey);
        }

        [TestMethod]
        [ExpectedException(typeof(SkillDoesNotExistException))]
        public void TestDeleteNonExistentSkill()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.DeleteSkill(EmployeeKey, 0);
        }

        [TestMethod]
        public void TestDeleteCert()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            var CertKey = ESSLogin.CreateCert(EmployeeKey, "ASP.NET Power User");
            ESSLogin.DeleteCert(EmployeeKey, CertKey);
        }

        [TestMethod]
        [ExpectedException(typeof(CertDoesNotExistException))]
        public void TestDeleteNonExistentCert()
        {
            var EmployeeKey = ESSLogin.CreateEmployee("First name", "Last name", "123 Address 1", "Line 2", "NY", "New York", "11111", "1234567890", "test@test.com");
            ESSLogin.DeleteCert(EmployeeKey, 0);
        }
    }
}