namespace Aniverse.MessageContracts.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContainerName { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
    }
}
