using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blockchain.Investments.Core.Model
{
    public abstract class BaseEntity
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
    }
}