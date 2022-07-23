using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Events.Message;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.API.Hubs
{
    public class ChatHub : Hub
    {
        readonly ISendEndpointProvider _sendEndpointProvider;
        public ChatHub(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task SendMessage(string userId, string message)
        {
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMqConstants.StateMachine}"));
            MessageCreatedEvent messageCreated = new()
            {
                Message = message,
                UserId = userId,
                SendDate = DateTime.UtcNow,
            };
            await sendEndpoint.Send<MessageCreatedEvent>(messageCreated);
            await Clients.User(userId).SendAsync("receiveMessage", message);
        }
        public async Task SendMessageAll(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
