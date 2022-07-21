using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Notfication.API.DataAccessLayer.Entities
{
    public class Notfication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public bool IsUnread { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }
        public string Url { get; set; }
    }
}
