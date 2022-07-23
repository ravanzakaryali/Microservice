using Aniverse.MessageContracts;
using MassTransit;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
