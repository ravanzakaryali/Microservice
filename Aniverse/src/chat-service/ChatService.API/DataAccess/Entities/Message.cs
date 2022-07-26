using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.API.DataAccess.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsDeleted { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
    }
}
