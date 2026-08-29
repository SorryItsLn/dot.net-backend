using AutoMapper;
using BookStore.Core.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, User>();
        CreateMap<User, UserEntity>();
    }
}

public class EmailConfirmationTokenMappingProfile : Profile
{
    public EmailConfirmationTokenMappingProfile()
    {
        CreateMap<EmailConfirmationTokenEntity, EmailConfirmationToken>();
    }
}
