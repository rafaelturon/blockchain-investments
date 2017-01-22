using System;
using CQRSlite.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blockchain.Investments.Core.Infrastructure.Domain
{
    public class BaseEvent : IEvent  
    {
        /// <summary>
        /// The ID of the Aggregate being affected by this event
        /// </summary>
        public Guid Id
        {
            get
            {
                return new Guid(AggregateId);
            }
            set
            {
                AggregateId = value.ToString();
            }
        }
        public string AggregateId {get; set;}
        private ObjectId _objectId;
        [BsonId]
        public ObjectId ObjectId 
        {
            get
            {
                if (_objectId.ToString().Equals("000000000000000000000000"))
                    _objectId = ObjectId.GenerateNewId();

                return _objectId;
            }
            set
            {
                _objectId = value;
            }
        }

        /// <summary>
        /// The Version of the Aggregate which results from this event
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The UTC time when this event occurred.
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }
    }
}