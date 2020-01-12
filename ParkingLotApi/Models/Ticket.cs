using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingLotApi.Models
{
    
    public class Ticket
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public DateTime GeneratedDate { get; set; }
        public Guid Number{get; set;}
        public double InitalParkingFee { get; set; }
        public double? FinalParkingFee { get; set; }

    }
}