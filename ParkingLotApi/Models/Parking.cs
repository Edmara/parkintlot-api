using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingLotApi.Models
{
    public class Parking
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate {get; set;}
        public Ticket Ticket {get; set; }
        public Lot Lot {get; set;}

    }
}