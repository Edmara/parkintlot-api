using MongoDB.Driver;
using ParkingLotApi.Models;

namespace ParkingLotApi.Database
{

    public class ParkingLotContext : IParkingLotContext
    {
        private readonly IMongoDatabase _db;
        public ParkingLotContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Lot> Lots => _db.GetCollection<Lot>("Lots");
        public IMongoCollection<Payment> Payments => _db.GetCollection<Payment>("Payments");
        public IMongoCollection<Parking> Parkings => _db.GetCollection<Parking>("Parkings");
        public IMongoCollection<Ticket> Tickets => _db.GetCollection<Ticket>("Tickets");


    }
}