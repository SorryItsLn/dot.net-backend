using BookStore.API.Extensions;
using BookStore.Application.Interfaces.Auth;
using BookStore.Application.Services;
using BookStore.Core.Abstractions;
using BookStore.Core.Constants;
using BookStore.Core.Enums;
using BookStore.DataAccess;
using BookStore.DataAccess.Mapping;
using BookStore.DataAccess.Repository;
using BookStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(BookStoreDbContext)));
});

builder.Services.AddApiAuthentication(configuration);
builder.Services.AddAuthorization();

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddAutoMapper(cfg => { }, typeof(UserMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("get", () => Results.Ok()).RequirePermissions(Permissions.Read);

app.MapPost("Post", () => Results.Ok()).RequirePermissions(Permissions.Create);

app.Run();
