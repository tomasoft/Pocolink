using MongoDB.Bson;

namespace Pocolink.Models.Models
{
    public class ShortenedUrl
    {
        public BsonObjectId Id { get; set; }
        public BsonString LUrl { get; set; }
        public BsonString SUrl { get; set; }
    }
}
