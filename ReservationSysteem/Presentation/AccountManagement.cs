
using  BCrypt.Net;
public class AccountManagement
{
    private AccountManagementLogic _logic = new();

    public void Start()
    {
        if (Session.CurrentUser == null)
        {
            Console.WriteLine("You must be logged in.");
            Console.ReadKey();
            StartMenu.Start();
            return;
        }

        string prompt = "Account Management";
        string[] options = { "Edit Account", "Delete Account", "Back" };
        Ui menu = new Ui(prompt, options);

        int choice = menu.Run();

        switch (choice)
        {
            case 0:
                EditAccount();
                break;

            case 1:
                string deletePrompt = "Delete Account";
                string[] deleteOptions = { "Confirm Delete", "Back" };
                Ui deleteMenu = new Ui(deletePrompt, deleteOptions);

                int deleteChoice = deleteMenu.Run();

                switch (deleteChoice)
                {
                    case 0:
                        DeleteAccount();
                        break;
                    case 1:
                        Start();
                        break;
                }
                break;

            case 2:
                string backPrompt = "Back Menu";
                string[] backOptions = { "Go to Visibility Menu", "Go to Start Menu" };
                Ui backMenu = new Ui(backPrompt, backOptions);

                int backChoice = backMenu.Run();

                switch (backChoice)
                {
                    case 0:
                        AccountVisibility.VisibilityMenu(Session.CurrentUser);
                        break;
                    case 1:
                        StartMenu.Start();
                        break;
                }
                break;
        }
    }

    private void EditAccount()
    {
        while (true)
        {
            Console.Clear();
            string prompt = "Edit Account";
            string[] options =
            {
                "Edit First Name",
                "Edit Last Name",
                "Edit Email",
                "Edit Phone",
                "Edit Password",
                "Save & Back"
            };

            Ui menu = new Ui(prompt, options);
            int choice = menu.Run();

            var user = Session.CurrentUser;

            switch (choice)
            {
                case 0:
                    Console.Write("New first name: ");
                    string firstName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(firstName))
                        user.FirstName = firstName;
                    break;

                case 1:
                    Console.Write("New last name: ");
                    string lastName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(lastName))
                        user.LastName = lastName;
                    break;

                case 2:
                    Console.Write("New email: ");
                    string email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email))
                        user.Email = email;
                    break;

                case 3:
                    Console.Write("New phone: ");
                    string phone = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(phone))
                        user.PhoneNumber = phone;
                    break;

                case 4:
                    Console.Write("New password: ");
                    string password = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(password))
                        user.Password = password;
                    break;

                case 5:
                    if (_logic.UpdateAccount(user))
                    {
                        Console.WriteLine("Account updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Update failed. Check your inputs.");
                    }
                    Console.ReadKey();
                    return;
            }
        }
    }

    private void DeleteAccount()
    {
        Console.Clear();
        Console.WriteLine("Enter your password to confirm deletion:");
        string password = Console.ReadLine();

        var user = Session.CurrentUser;

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            Console.WriteLine("Incorrect password.");
            Console.ReadKey();
            return;
        }

        _logic.DeleteAccount(user.Id);

        Session.CurrentUser = null;
        Console.WriteLine("Account deleted successfully.");
        Console.ReadKey();

        StartMenu.Start();
    }
}