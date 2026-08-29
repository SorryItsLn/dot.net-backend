namespace BookStore.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public required string UserName { get; set; }

        public required string PasswordHash { get; set; }

        public required string Email { get; set; }

        public bool EmailConfirmed { get; set; } = false;
        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
