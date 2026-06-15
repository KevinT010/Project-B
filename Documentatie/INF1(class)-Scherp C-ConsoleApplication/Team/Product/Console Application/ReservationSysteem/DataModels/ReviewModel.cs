public class ReviewModel
{
    public Int64 Id { get; set; }
    public Int64 AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public ReviewModel()
    {
    }

    public ReviewModel(Int64 accountId, int rating, string comment)
    {
        AccountId = accountId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.Now;
    }
}