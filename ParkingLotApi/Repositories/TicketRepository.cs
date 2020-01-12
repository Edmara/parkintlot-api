using ParkingLotApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using ParkingLotApi.Database;

namespace ParkingLotApi.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IParkingLotContext _context;
        public TicketRepository(IParkingLotContext context)
        {
            _context = context;
        }
        public IEnumerable<Ticket> GetAllTickets()
        {
            return _context
                            .Tickets
                            .Find(_ => true)
                            .ToList();
        }
        public Ticket GetTicket(long id)
        {
            FilterDefinition<Ticket> filter = Builders<Ticket>.Filter.Eq(m => m.Id, id);
            return _context
                    .Tickets
                    .Find(filter)
                    .FirstOrDefault();
        }
        public Task Create(Ticket payments)
        {
            return _context.Tickets.InsertOneAsync(payments);
        }
        public bool Update(Ticket payments)
        {
            ReplaceOneResult updateResult =
                _context
                        .Tickets
                        .ReplaceOne(
                            filter: g => g.Id == payments.Id,
                            replacement: payments);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public bool Delete(long id)
        {
            FilterDefinition<Ticket> filter = Builders<Ticket>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = _context
                                              .Tickets
                                              .DeleteOne(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public long GetNextId()
        {
            return _context.Tickets.CountDocuments(new BsonDocument()) + 1;
        }
    }
}