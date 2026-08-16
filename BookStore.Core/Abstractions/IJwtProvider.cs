using BookStore.Core.Models;

namespace BookStore.Core.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
