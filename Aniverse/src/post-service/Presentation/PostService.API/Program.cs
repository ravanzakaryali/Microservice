using Microsoft.EntityFrameworkCore;
using PostService.Persistence.DataContext;
using PostService.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
string authenticationProviderKey = "TestKey";
SymmetricSecurityKey signInKey = new(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Security"]));
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = authenticationProviderKey;
})
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signInKey,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            RequireExpirationTime = true
        };
    });
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
