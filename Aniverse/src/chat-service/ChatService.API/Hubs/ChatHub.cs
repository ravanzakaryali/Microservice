using ChatService.API.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IRabbitMqService _mqService;
        public async Task SendMessage(string userId,string message)
        {
            await Clients.User(userId).SendAsync("receiveMessage", message);
        }
    }
}
