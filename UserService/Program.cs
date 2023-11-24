using UserService.Data;
using Microsoft.EntityFrameworkCore;
using UserService.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = DBUser; Integrated Security = True"));
//builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
