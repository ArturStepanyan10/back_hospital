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

builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = DBAllUserst4; Integrated Security = True"));
//builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Конфигурация параметров Identity
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
        .AddEntityFrameworkStores<UserDbContext>()
        .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});


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

//app.MapIdentityApi<User>();

app.Run();
