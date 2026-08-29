using AutoMapper;
using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Enums;
using BookStore.Core.Helpers;
using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BookStore.DataAccess.Repository
{
    public class UserRepository(BookStoreDbContext context, IMapper mapper) : IUserRepository
    {
        private readonly BookStoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<User>> AddUser(User user)
        {
            var roleEntity =
                await _context.Roles.SingleOrDefaultAsync(r => r.Id == (int)user.Role)
                ?? throw new InvalidOperationException("Role not be found");

            var entity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Roles = [roleEntity],
            };

            await _context.Users.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
                return Result<User>.Success(user);
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                string errorMessage = $"User with email '{user.Email}' already exists.";
                return Result<User>.Failure(errorMessage);
            }
        }

        public async Task<User> GetById(Guid userId)
        {
            var userEntity =
                await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new Exception("User not found");

            return _mapper.Map<User>(userEntity);
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

        private static bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            return ex.InnerException is PostgresException { SqlState: "23505" };
        }
    }
}
