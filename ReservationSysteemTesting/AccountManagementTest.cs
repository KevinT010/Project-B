
[TestClass]
public class AccountManagementTest
{
    [TestMethod]
    public void FirstNameValidation_ValidName_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string FirstName = "john";
        AccountModel result = accountManagementLogic.FirstNameValidation(FirstName);

        Assert.IsTrue(result != null);
    }

    [TestMethod]
    public void FirstNameValidation_InvalidName_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string FirstName = "j";
        AccountModel result = accountManagementLogic.FirstNameValidation(FirstName);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void LastNameValidation_ValidName_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string LastName = "doe";
        AccountModel result = accountManagementLogic.LastNameValidation(LastName);

        Assert.IsTrue(result != null);
    }

    [TestMethod]
    public void LastNameValidation_InvalidName_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe2@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string LastName = "d";
        AccountModel result = accountManagementLogic.LastNameValidation(LastName);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void EmailValidation_ValidEmail_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string Email = "johndoe@example.com";
        AccountModel result = accountManagementLogic.EmailValidation(Email);

        Assert.IsTrue(result != null);
    }
    [TestMethod]
    public void EmailValidation_InvalidEmail_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string Email = "invalidemail";
        AccountModel result = accountManagementLogic.EmailValidation(Email);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void PhoneNumberValidation_ValidPhoneNumber_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "0612345678", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string PhoneNumber = "06123456789";
        AccountModel result = accountManagementLogic.PhoneNumberValidation(PhoneNumber);

        Assert.IsTrue(result != null);
    }
    [TestMethod]
    public void PhoneNumberValidation_InvalidPhoneNumber_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string PhoneNumber = "123";
        AccountModel result = accountManagementLogic.PhoneNumberValidation(PhoneNumber);

        Assert.IsFalse(result != null);
    }

    [TestMethod]
    public void PasswordValidation_ValidPassword_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string Password = "password1234";
        AccountModel result = accountManagementLogic.PasswordValidation(Password);

        Assert.IsTrue(result != null);
    }
    [TestMethod]
    public void PasswordValidation_InvalidPassword_ReturnsFalse()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string Password = "123";
        AccountModel result = accountManagementLogic.PasswordValidation(Password);

        Assert.IsFalse(result != null);
    }


    [TestMethod]
    public void UpdateAccount_ValidInputs_ReturnsTrue()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        string FirstName = "test1234";
        string LastName = "test1234";
        string Email = "test1234@gmail.com";
        string PhoneNumber = "061234578";
        string Password = "12345678";

        AccountModel updatedAccount = new AccountModel(FirstName, LastName, Email, PhoneNumber, Password, 1, 0);
        bool result = accountManagementLogic.UpdateAccount(updatedAccount);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DeleteAccount_ValidId_DeletesAccount()
    {
        AccountManagementLogic accountManagementLogic = new AccountManagementLogic();
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0);
        Session.CurrentUser = testUser;
        int accountId = 1;
        bool result = accountManagementLogic.DeleteAccount(accountId);

        Assert.IsTrue(result);
        Assert.IsNull(Session.CurrentUser);
    }

}