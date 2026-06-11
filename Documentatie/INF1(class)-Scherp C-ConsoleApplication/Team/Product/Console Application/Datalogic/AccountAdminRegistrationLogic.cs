public class AccountAdminRegistrationLogic
{
    private new AccountRegistrationLogic logic = new AccountRegistrationLogic();
    private AccountRegistrationAccess _access = new AccountRegistrationAccess();


    public bool FirstNameValidation(string firstName)
    {
        return logic.FirstNameValidation(firstName);
    }

    public bool LastNameValidation(string lastName)
    {
        return logic.LastNameValidation(lastName);
    }

    public bool EmailValidation(string email)
    {
        return logic.EmailValidation(email);
    }

    public bool PhoneNumberValidation(string phoneNumber)
    {
        return logic.PhoneNumberValidation(phoneNumber);
    }

    public bool PasswordValidation(string password)
    {
        return logic.PasswordValidation(password);
    }

    public bool AccountRegistrationValidation(string firstName, string lastName, string email, string phoneNumber, string password)
    {
        if (FirstNameValidation(firstName) && LastNameValidation(lastName) && EmailValidation(email) && PhoneNumberValidation(phoneNumber) && PasswordValidation(password))
        {
            _access.InsertAccount(new AccountModel(firstName, lastName, email, phoneNumber, BCrypt.Net.BCrypt.HashPassword(password), 2, 0, 0));
            return true;
        }
        else
        {
            return false;
        }
    }

}