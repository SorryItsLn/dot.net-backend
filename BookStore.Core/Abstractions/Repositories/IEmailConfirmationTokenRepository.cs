using BookStore.Core.Models;

namespace BookStore.Core.Abstractions.Repositories;

public interface IEmailConfirmationTokenRepository
{
    Task<EmailConfirmationToken> CreateToken(string token, Guid userId);
}
