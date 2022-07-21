using ChatService.API.DTOs;
using RabbitMQ.Client;

namespace ChatService.API.Service.Interfaces
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }
}
