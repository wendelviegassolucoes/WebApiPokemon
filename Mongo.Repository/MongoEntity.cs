using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Repository
{
    [DataContract]
    [BsonIgnoreExtraElements(Inherited = true)]
    [Serializable]
    public abstract class MongoEntity
    {
        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}