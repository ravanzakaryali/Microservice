using AuthService.API.DataAccessLayer.DB;
using AuthService.API.DataAccessLayer.Entites;
using AuthService.API.Service.Abstractions;
using AuthService.API.Service.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;
    
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAutheticateService, AutheticateService>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
