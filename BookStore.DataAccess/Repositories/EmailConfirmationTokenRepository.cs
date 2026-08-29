using AutoMapper;
using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess;

public class EmailConfirmationTokenRepository(BookStoreDbContext context, IMapper mapper)
    : IEmailConfirmationTokenRepository
{
    private readonly IMapper _mapper = mapper;

    public async Task<EmailConfirmationToken> CreateToken(string token, Guid userId)
    {
        var emailConfirmationToken = new EmailConfirmationTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
        };
        await context.EmailConfirmationToken.AddAsync(emailConfirmationToken);
        await context.SaveChangesAsync();

        return _mapper.Map<EmailConfirmationToken>(emailConfirmationToken);
    }
}
