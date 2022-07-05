using Aniverse.MessageContracts;
using FileService.API.Consumers;
using FileService.API.DataAccess.DB;
using FileService.API.Services;
using FileService.API.Services.Abstractions.MongoDb;
using FileService.API.Services.Abstractions.Storage;
using FileService.API.Services.Implementations.MongoDb;
using FileService.API.Services.Implementations.Storage;
using FileService.API.Services.Implementations.Storage.Azure;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<PostFileConsumer>();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<PostFileConsumer>();
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(RabbitMqConstants.URI), h =>
        {
            h.Username(RabbitMqConstants.Username);
            h.Password(RabbitMqConstants.Password);
        });
        cfg.ReceiveEndpoint(RabbitMqConstants.FileServiceSendQueue, ep =>
        {
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.Consumer<PostFileConsumer>(context);
        });
    });
});


string authenticationProviderKey = "TestKey";
builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = authenticationProviderKey)
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Security"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
        };
    });
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<IMongoDbService, MongoDbService>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddControllers();
builder.Services.AddScoped<IStorageService, StorageService>();

var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
