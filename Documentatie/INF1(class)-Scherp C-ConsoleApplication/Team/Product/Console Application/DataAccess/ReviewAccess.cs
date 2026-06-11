using Microsoft.Data.Sqlite;
using Dapper;

public class ReviewAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");
    private string ReviewTable = "Review";

    public void InsertReview(ReviewModel review)
    {
        string query = $"INSERT INTO {ReviewTable} (AccountId, Rating, Comment, CreatedAt) VALUES (@AccountId, @Rating, @Comment, @CreatedAt)";
        _connection.Execute(query, review);
    }

    public List<ReviewModel> GetAllReviews()
    {
        string query = $@"SELECT rev.*, acc.FirstName, acc.LastName 
                          FROM {ReviewTable} rev
                          LEFT JOIN Account acc ON rev.AccountId = acc.Id
                          ORDER BY rev.CreatedAt DESC";
        return _connection.Query<ReviewModel>(query).ToList();
    }

    public List<ReviewModel> GetByAccountId(Int64 accountId)
    {
        string query = $"SELECT * FROM {ReviewTable} WHERE AccountId = @AccountId ORDER BY CreatedAt DESC";
        return _connection.Query<ReviewModel>(query, new { AccountId = accountId }).ToList();
    }

    public void DeleteReview(Int64 reviewId)
    {
        string query = $"DELETE FROM {ReviewTable} WHERE Id = @Id";
        _connection.Execute(query, new { Id = reviewId });
    }

    public void DeleteReviewbyaccount(Int64 accountId)
    {
        string query = $"DELETE FROM {ReviewTable} WHERE AccountId = @AccountId";
        _connection.Execute(query, new { AccountId = accountId });
    }
}