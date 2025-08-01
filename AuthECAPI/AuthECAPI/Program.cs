using AuthECAPI;
using AuthECAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services from Identity Core
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();
//Validations
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
});

//Db Connection to SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//to prevent cors issue 4200 is the url of the angular app
app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app
    .MapGroup("/api")
    .MapIdentityApi<AppUser>();

// Endpoint for user registration
app.MapPost("/api/signup", async (
    UserManager<AppUser> userManager,
    [FromBody] UserRegistrationModel userRegistrationModel) =>
    {
        AppUser user = new AppUser()
        {
            UserName = userRegistrationModel.Email,
            Email = userRegistrationModel.Email,
            FullName = userRegistrationModel.FullName
        };
        var result = await userManager.CreateAsync(user, userRegistrationModel.Password);

        if (result.Succeeded)
            return Results.Ok(result);
        else
            return Results.BadRequest(result);
    });

app.Run();

public class UserRegistrationModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
}
