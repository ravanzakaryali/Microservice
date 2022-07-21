using ChatService.API.DTOs;
using ChatService.API.Service.Interfaces;
using RabbitMQ.Client;

namespace ChatService.API.Service.Implementations
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;

        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new()
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
