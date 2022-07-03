namespace Aniverse.MessageContracts
{
    public class RabbitMqConstants
    {
        public const string URI = "amqp://guest:guest@localhost:5672";
        public const string Username = "guest";
        public const string Password = "guest";
        public const int Port = 5672;
        public const bool IsSSL = false;
        public const string NotificationServiceQueue = "notification.service";
    }
}
