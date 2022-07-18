namespace Aniverse.MessageContracts
{
    public class RabbitMqConstants
    {
        public const string URI = "amqp://guest:guest@localhost:5672";
        public const string Username = "guest";
        public const string Password = "guest";
        public const int Port = 5672;
        public const bool IsSSL = false;
        public const string StateMachine = "state-machine-queue";
        public const string NotificationServiceQueue = "notification.service";
        public const string FileServiceSendQueue = "file-started-queue";
        public const string SendNotfication = $"{URI}/{NotificationServiceQueue}";
        public const string SendFileService = $"{URI}/{ FileServiceSendQueue}";
    }
}
