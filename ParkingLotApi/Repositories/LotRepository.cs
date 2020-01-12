using ParkingLotApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using ParkingLotApi.Database;

namespace ParkingLotApi.Repositories
{
    public class LotRepository : ILotRepository
    {
        private readonly IParkingLotContext _context;
        public LotRepository(IParkingLotContext context)
        {
            _context = context;
        }
        public  IEnumerable<Lot> GetAllLots()
        {
            IEnumerable<Lot> lotsList =  _context.Lots.Find(_ => true).ToList();

            return lotsList;
        }
        public Lot GetLot(long id)
        {
 
            FilterDefinition<Lot> filter = Builders<Lot>.Filter.Eq(m => m.Id, id);
            return _context
                    .Lots
                    .Find(filter)
                    .FirstOrDefault();

        }
        public void Create(Lot lot)
        {
            _context.Lots.InsertOne(lot);
        }
        public bool Update(Lot lot)
        {

            ReplaceOneResult updateResult =
                 _context
                        .Lots
                        .ReplaceOne(
                            filter: g => g.Id == lot.Id,
                            replacement: lot);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;

        }
        public bool Delete(long id)
        {
            FilterDefinition<Lot> filter = Builders<Lot>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = _context
                                              .Lots
                                              .DeleteOne(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;

        }
        public long GetNextId()
        {
            // return await _context.Lots.CountDocumentsAsync(new BsonDocument()) + 1;
            return _context.Lots.CountDocuments(new BsonDocument()) + 1;            
        }

        public Lot getEmptyLot()
        {
            FilterDefinition<Lot> filter = Builders<Lot>.Filter.Eq(m => m.Busy, false);
            return _context
                    .Lots
                    .Find(filter)
                    .FirstOrDefault();            
        }

        public IEnumerable<Lot> getEmptiesLots()
        {
            FilterDefinition<Lot> filter = Builders<Lot>.Filter.Eq(m => m.Busy, false);
            IEnumerable<Lot> lotsList =  _context.Lots.Find(filter).ToList();

            return lotsList;

        }

    }
}