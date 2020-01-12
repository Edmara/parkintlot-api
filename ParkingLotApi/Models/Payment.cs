using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingLotApi.Models
{
    
    public class Payment
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public DateTime PaidDate { get; set; }
        public double TotalPaid { get; set; }
        public Ticket Ticket { get; set; }
    }
}