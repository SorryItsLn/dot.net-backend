using AutoMapper;
using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository
{
    public class UserRepository(BookStoreDbContext context, IMapper mapper) : IUserRepository
    {
        private readonly BookStoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<User> AddUser(User user)
        {
            var entity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity =
                await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new Exception();
            System.Console.WriteLine(userEntity);
            return _mapper.Map<User>(userEntity);
        }
    }
}
