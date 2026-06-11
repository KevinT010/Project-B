public class Review
{
    private ReviewLogic _logic = new();

    public void Start(AccountModel account)
    {
        if(account.IsAdmin)
        {
            string[] options = { "View all reviews", "Go back" };
            Ui reviewMenu = new Ui("Reviews", options);
            int selectedIndex = reviewMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    ViewAllReviews(account);
                    break;
                case 1:
                    AccountVisibility.VisibilityMenu(account);
                    break;
            }
        }

        else
        {
            string[] options = { "Write a review", "View my reviews", "View all reviews", "Go back" };
            Ui reviewMenu = new Ui("Reviews", options);
            int selectedIndex = reviewMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    WriteReview(account);
                    break;
                case 1:
                    ViewMyReviews(account);
                    break;
                case 2:
                    ViewAllReviews(account);
                    break;
                case 3:
                    AccountVisibility.VisibilityMenu(account);
                    break;
            }
        }
    }

    private void WriteReview(AccountModel account)
    {
        Console.Clear();
        Console.WriteLine("Rate your experience (1-5 stars), or type Cancel to return to go back:");
        Console.WriteLine("1 = ★☆☆☆☆  2 = ★★☆☆☆  3 = ★★★☆☆  4 = ★★★★☆  5 = ★★★★★");

        int rating = 0;
        while (rating < 1 || rating > 5)
        {
            Console.Write("Enter rating: ");
            string ratingInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(ratingInput) && ratingInput.Trim().ToLower() == "cancel")
            {
                Start(account);
                return;
            }

            if (!int.TryParse(ratingInput, out rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Please enter a number between 1 and 5.");
                rating = 0;
            }
        }

        Console.WriteLine("Write your comment, or press Enter to skip, or type Cancel to go back):");
        string comment = Console.ReadLine();
        if(comment == null)
        {
            comment = "";
        }

        if (comment.Trim().ToLower() == "cancel")
        {
            Start(account);
            return;
        }

        bool success = _logic.PostReview(account, rating, comment);

        Console.Clear();
        if (success)
        {
            Console.WriteLine($"✅ Review submitted! {_logic.GetStars(rating)}");
        }
        else
        {
            Console.WriteLine("❌ Your review could not be submitted. Please check your input.");
        }

        Thread.Sleep(2000);
        Start(account);
    }

    private void ViewMyReviews(AccountModel account)
    {
        Console.Clear();
        var reviews = _logic.GetByAccountId(account.Id);

        if (reviews.Count == 0)
        {
            Console.WriteLine("You currently don't have any reviews.");
            Thread.Sleep(2000);
            Start(account);
            return;
        }

        string[] options = new string[reviews.Count + 1];
        for (int i = 0; i < reviews.Count; i++)
        {
            if(reviews[i].Comment.Length > 100)
            {
                options[i] = $"{_logic.GetStars(reviews[i].Rating)} | {reviews[i].FirstName} {reviews[i].LastName} | {reviews[i].CreatedAt:dd-MM-yyyy} | {reviews[i].Comment.Substring(0, 100)}...";
            }
            else
            {
                options[i] = $"{_logic.GetStars(reviews[i].Rating)} | {reviews[i].FirstName} {reviews[i].LastName} | {reviews[i].CreatedAt:dd-MM-yyyy} | {reviews[i].Comment}";
            }
        }
        options[reviews.Count] = "Go back";

        Ui reviewList = new Ui("My reviews", options);
        int selectedIndex = reviewList.Run();

        if (selectedIndex == reviews.Count)
        {
            Start(account);
            return;
        }

        ReviewModel selected = reviews[selectedIndex];
        string[] actions = { "Delete review", "Go back" };
        Ui actionMenu = new Ui($"{_logic.GetStars(selected.Rating)} - {selected.Comment}", actions);
        int actionIndex = actionMenu.Run();

        if (actionIndex == 0)
        {
            _logic.DeleteReview(selected.Id);
            Console.Clear();
            Console.WriteLine("Review deleted.");
            Console.WriteLine("Returning to the review menu");
            Thread.Sleep(2000);
        }

        Start(account);
    }

    private void ViewAllReviews(AccountModel account)
    {
        Console.Clear();
        var reviews = _logic.GetAllReviews();

        if (reviews.Count == 0)
        {
            Console.WriteLine("There are no reviews yet.");
            Console.WriteLine("Returning to the review menu");
            Thread.Sleep(2000);
            Start(account);
            return;
        }

        string[] options = new string[reviews.Count + 1];
        for (int i = 0; i < reviews.Count; i++)
        {
            if(reviews[i].Comment.Length > 100)
            {
                options[i] = $"{_logic.GetStars(reviews[i].Rating)} | {reviews[i].FirstName} {reviews[i].LastName} | {reviews[i].CreatedAt:dd-MM-yyyy} | {reviews[i].Comment.Substring(0, 100)}...";
            }
            else
            {
                options[i] = $"{_logic.GetStars(reviews[i].Rating)} | {reviews[i].FirstName} {reviews[i].LastName} | {reviews[i].CreatedAt:dd-MM-yyyy} | {reviews[i].Comment}";
            }
        }
        options[reviews.Count] = "Go back";

        Ui reviewList = new Ui("All reviews", options);
        int selectedIndex = reviewList.Run();

        if (selectedIndex == reviews.Count)
        {
            Start(account);
            return;
        }

        ReviewModel selected = reviews[selectedIndex];
        string[] actions = { "Go back" };
        Ui ViewMenu = new Ui($"{_logic.GetStars(selected.Rating)} | {selected.FirstName} {selected.LastName}\n{selected.Comment}", actions);
        ViewMenu.Run();

        Start(account);
    }
}