using System;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Model
{
    public class IEntity
    {
        public ObjectId Id { get; set; }
    }
}