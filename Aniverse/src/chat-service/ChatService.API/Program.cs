using Aniverse.MessageContracts;
using ChatService.API.Consumers;
using ChatService.API.DataAccess.DB;
using ChatService.API.Hubs;
using ChatService.API.Services.Abstractions.MongoDb;
using ChatService.API.Services.Implementations.MongoDb;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<MessageConsumer>();
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(RabbitMqConstants.URI), h =>
        {
            h.Username(RabbitMqConstants.Username);
            h.Password(RabbitMqConstants.Password);
        });
        cfg.ReceiveEndpoint(RabbitMqConstants.SendMessageQueue, ep =>
        {
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.Consumer<MessageConsumer>(context);
        });
    });
});

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

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

var app = builder.Build();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
});
app.UseHttpsRedirection();
app.Run();
