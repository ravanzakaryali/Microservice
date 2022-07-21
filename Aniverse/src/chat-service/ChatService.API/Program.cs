using ChatService.API.DTOs;
using ChatService.API.Service.Implementations;
using ChatService.API.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RabbitMqConfiguration>(a => builder.Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
