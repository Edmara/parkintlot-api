using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingLotApi.Models
{

    public class Lot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Busy { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }

    }
}