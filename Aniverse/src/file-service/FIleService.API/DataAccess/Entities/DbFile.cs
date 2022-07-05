using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FileService.API.DataAccess.Entities
{
    public class DbFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string? PostId { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
        [BsonDefaultValue(false)]
        public bool IsDeleted { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public string DataType { get; set; }
        public string StorageUrl { get; set; }
        [BsonDefaultValue(false)]
        public bool IsProfile { get; set; }
        [BsonDefaultValue(false)]
        public bool IsCover { get; set; }
        public string VideoLength { get; set; }
    }
}
