using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blockchain.Investments.Core.Model
{
    public class IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}