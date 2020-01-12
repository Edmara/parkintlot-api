using ParkingLotApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using ParkingLotApi.Database;

namespace ParkingLotApi.Repositories
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly IParkingLotContext _context;
        public ParkingRepository(IParkingLotContext context)
        {
            _context = context;
        }
        public IEnumerable<Parking> GetAllParkings()
        {
            return _context
                            .Parkings
                            .Find(_ => true)
                            .ToList();
        }
        public Parking GetParking(long id)
        {
            FilterDefinition<Parking> filter = Builders<Parking>.Filter.Eq(m => m.Id, id);
            return _context
                    .Parkings
                    .Find(filter)
                    .FirstOrDefault();
        }
        public void Create(Parking parking)
        {
             _context.Parkings.InsertOne(parking);
        }
        public bool Update(Parking parking)
        {
            ReplaceOneResult updateResult =
                 _context
                        .Parkings
                        .ReplaceOne(
                            filter: g => g.Id == parking.Id,
                            replacement: parking);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public bool Delete(long id)
        {
            FilterDefinition<Parking> filter = Builders<Parking>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = _context
                                              .Parkings
                                              .DeleteOne(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public long GetNextId()
        {
            return _context.Parkings.CountDocuments(new BsonDocument()) + 1;
        }

        public IEnumerable<Parking> GetActualParkings()
        {
            return _context
                    .Parkings
                    .Find(p => p.ExitDate == null)
                    .ToList();
        }
    }
}