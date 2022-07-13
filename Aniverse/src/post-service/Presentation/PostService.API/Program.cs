using Microsoft.EntityFrameworkCore;
using PostService.Persistence.DataContext;
using PostService.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;
using Aniverse.MessageContracts;
using Notfication.API.Consumers;
using PostService.Infrastructure.Implementations.Stroage.Azure;
using PostService.Infrastructure;
using PostService.Infrastructure.Abstractions.Storage;
using PostService.Infrastructure.Implementations.Stroage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri(RabbitMqConstants.URI), h =>
        {
            h.Username(RabbitMqConstants.Username);
            h.Password(RabbitMqConstants.Password);
        });
    }));
}); 

builder.Services.AddMassTransitHostedService();

builder.Services.AddStorage<AzureStorage>();

builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddAppServices();

string authenticationProviderKey = "TestKey";

builder.Services.AddAuthentication(options => options.DefaultAuthenticateScheme = authenticationProviderKey)
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Security"])),
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
