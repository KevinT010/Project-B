public class ReviewLogic
{
    private ReviewAccess _access = new();

    public bool PostReview(AccountModel account, int rating, string comment)
    {
        if (rating < 1 || rating > 5)
        {
            return false;
        }
        _access.InsertReview(new ReviewModel(account.Id, rating, comment));
        return true;
    }

    public List<ReviewModel> GetAllReviews()
    {
        return _access.GetAllReviews();
    }

    public List<ReviewModel> GetByAccountId(Int64 accountId)
    {
        return _access.GetByAccountId(accountId);
    }

    public void DeleteReview(Int64 reviewId)
    {
        _access.DeleteReview(reviewId);
    }

    public string GetStars(int rating)
    {
        return new string('★', rating) + new string('☆', 5 - rating);
    }
}