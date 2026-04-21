public class AccountModel
{

    public Int64 Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public int AccountLevel { get; set; }

    public int Points { get; set; }

    public AccountModel()
    {
    }

    public AccountModel(string firstName, string lastName, string email, string phoneNumber, string password, int accountLevel, int points)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        AccountLevel = accountLevel;
        Points = points;
    }


}



