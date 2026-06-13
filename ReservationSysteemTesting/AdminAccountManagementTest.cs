

[TestClass]
public class AdminAccountManagementTest
{
    [TestMethod]
    public void GetUserByEmail_ValidEmail_ReturnsAccountModel()
    {
        var logic = new AccountManagementLogic();
        var email = "test2@gmail.com";
        var result = logic.GetUserByEmail(email);
        Assert.IsNotNull(result);
        Assert.AreEqual(email, result.Email);

    }

    [TestMethod]
    public void GetUserByEmail_InvalidEmail_ReturnsNull()
    {
        var logic = new AccountManagementLogic();
        var email = "JohnDoe@gmail.com";
        var result = logic.GetUserByEmail(email);
        Assert.IsNull(result);
        Assert.AreNotEqual(email, result?.Email);
    }

    [TestMethod]
    public void FirstNameValidation_ValidName_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string FirstName = "John";
        AccountModel result = accountManagementLogic.FirstNameValidation(FirstName);

        Assert.IsTrue(result != null);
    }

    [TestMethod]
    public void FirstNameValidation_InvalidName_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string FirstName = "J";
        AccountModel result = accountManagementLogic.FirstNameValidation(FirstName);

        Assert.IsFalse(result != null);
    }
    [TestMethod]
    public void FirstNameValidation_InvalidName_ReturnsFalse_part2()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string FirstName = "Rhoshandiatellyneshiaunneveshenk";
        AccountModel result = accountManagementLogic.FirstNameValidation(FirstName);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void LastNameValidation_ValidName_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe1", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string LastName = "Doe";
        AccountModel result = accountManagementLogic.LastNameValidation(LastName);

        Assert.IsTrue(result != null);
    }

    [TestMethod]
    public void LastNameValidation_InvalidName_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe1", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string LastName = "j";
        AccountModel result = accountManagementLogic.LastNameValidation(LastName);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void LastNameValidation_InvalidName_ReturnsFalse_part2()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe1", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string LastName = "Wolfeschlegelsteinhausenbergerdorff";
        AccountModel result = accountManagementLogic.LastNameValidation(LastName);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void EmailValidation_ValidEmail_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string Email = "Johndoe@gmail.com";
        AccountModel result = accountManagementLogic.EmailValidation(Email);

        Assert.IsTrue(result != null);
    }

    [TestMethod]
    public void EmailValidation_InvalidEmail_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string Email = "Johndoegmail.com";
        AccountModel result = accountManagementLogic.EmailValidation(Email);

        Assert.IsFalse(result != null);

    }

    [TestMethod]
    public void PhoneNumberValidation_ValidPhoneNumber_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "0612345678", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string PhoneNumber = "0612345";
        AccountModel result = accountManagementLogic.PhoneNumberValidation(PhoneNumber);
        Assert.IsTrue(result != null);
    }

    [TestMethod]
    public void PhoneNumberValidation_InvalidPhoneNumber_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "0612345678", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        string PhoneNumber = "1234";
        AccountModel result = accountManagementLogic.PhoneNumberValidation(PhoneNumber);
        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void DeleteAccount_ValidId_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "0612345678", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        long id = 1;
        bool result = accountManagementLogic.DeleteAccount(id);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DeleteAccount_InvalidId_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        long accountId = 999;
        bool result = accountManagementLogic.DeleteAccount(accountId);

        Assert.IsNotNull(Session.CurrentUser);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void UpdateAccount_ValidAccount_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        testUser.FirstName = "test1234";
        testUser.LastName = "test1234";
        testUser.Email = "test1234@gmail.com";
        testUser.PhoneNumber = "061234578";
        accountManagementLogic.UpdateAccount(testUser);
        Assert.IsTrue(testUser.FirstName == "test1234");
        Assert.IsTrue(testUser.LastName == "test1234");
        Assert.IsTrue(testUser.Email == "test1234@gmail.com");
        Assert.IsTrue(testUser.PhoneNumber == "061234578");
    }


}
