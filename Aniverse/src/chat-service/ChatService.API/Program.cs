using ChatService.API.DTOs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
