using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notfication.API.DataAccessLayer.Entities
{
    public class Notfication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string UserId { get; set; }
    }
}
