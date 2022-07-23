using Aniverse.MessageContracts;
using ChatService.API.Consumers;
using MassTransit;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
