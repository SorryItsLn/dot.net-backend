using System.Security.Cryptography;
using BookStore.Core;
using BookStore.Core.Abstractions;
using BookStore.Core.Abstractions.Auth;
using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Helpers;
using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IEmailConfirmationTokenRepository emailConfirmationTokenRepository
    ) : IUserService
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUserRepository _userRepository = userRepository;

        private readonly IEmailConfirmationTokenRepository _confirmationTokenRepository =
            emailConfirmationTokenRepository;

        private readonly IJwtProvider _iJwtProvider = jwtProvider;

        public async Task<Result<User>> Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

            return await _userRepository.AddUser(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _iJwtProvider.GenerateToken(user);

            return token;
        }

        public async Task<Result<EmailConfirmationToken>> SendConfirmationEmail(Guid userId)
        {
            var user = await _userRepository.GetById(userId); //TODO: поправить на обычную проверку
            if (user is null)
            {
                return Result<EmailConfirmationToken>.Failure("User not found");
            }

            var token = GenerateSecureToken();
            try
            {
                var emailConfirmToken = await _confirmationTokenRepository.CreateToken(
                    token,
                    userId
                );

                return Result<EmailConfirmationToken>.Success(emailConfirmToken);
            }
            catch (Exception ex)
            {
                return Result<EmailConfirmationToken>.Failure(
                    $"Can't create confirmation token for user - {ex}"
                );
            }
        }

        private static string GenerateSecureToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert
                .ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}
