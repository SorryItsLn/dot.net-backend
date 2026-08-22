using BookStore.Application.Interfaces.Auth;
using BookStore.Core.Abstractions;
using BookStore.Core.Helpers;
using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider
    ) : IUserService
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUserRepository _userRepository = userRepository;
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

        // public async Task<string> SetRole(Guid userId)
        // {
        //     var user =

        //     return
        // }
    }
}
