namespace BookStore.Core.Models;

public class EmailConfirmationToken
{
    private EmailConfirmationToken(Guid id, Guid userId, string token, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        IsUsed = false;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;

    public static EmailConfirmationToken Create(
        Guid id,
        Guid userId,
        string token,
        DateTime expiresAt
    )
    {
        return new EmailConfirmationToken(id, userId, token, expiresAt);
    }
}
