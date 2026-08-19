using AutoMapper;
using BookStore.Core.Abstractions;
using BookStore.Core.Enums;
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
            var role = user.UserName == "Admin" ? Role.Admin : Role.User;

            var roleEntity =
                await _context.Roles.SingleOrDefaultAsync(r => r.Id == (int)role)
                ?? throw new InvalidOperationException();

            Console.WriteLine($"Role - {role}, roleEntity - {roleEntity} ");
            var entity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Roles = [roleEntity],
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity =
                await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new Exception("User not found");
            return _mapper.Map<User>(userEntity);
        }

        public async Task<HashSet<Permissions>> GetUserPermissions(Guid userId)
        {
            var roles = await _context
                .Users.AsNoTracking()
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permissions)p.Id)
                .ToHashSet();
        }
    }
}
