namespace ChatService.API.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage();
        Task GetClients();   

    }
}
