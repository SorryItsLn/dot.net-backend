using BookStore.API.Extensions;
using BookStore.Application.Extensions;
using BookStore.DataAccess.Extensions;
using BookStore.DataAccess.Mapping;
using BookStore.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services.AddDataAccess(builder.Configuration)
    .AddApplicationScope()
    .AddInfrastructureScope(builder.Configuration);

builder.Services.AddApiAuthentication(configuration);
builder.Services.AddAuthorization();

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

app.Run();
