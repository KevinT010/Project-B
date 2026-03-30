public class AccountModel
{

    public string FullName { get; set; }
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public AccountModel( string fullname, string email, string phoneNumber, string password)
    {
        FullName = fullname;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }


}



