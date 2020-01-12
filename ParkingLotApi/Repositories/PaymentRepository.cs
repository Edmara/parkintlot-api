using ParkingLotApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System;
using ParkingLotApi.Database;

namespace ParkingLotApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IParkingLotContext _context;
        public PaymentRepository(IParkingLotContext context)
        {
            _context = context;
        }
        public IEnumerable<Payment> GetAllPayments()
        {
            return _context
                            .Payments
                            .Find(_ => true)
                            .ToList();
        }
        public Payment GetPayment(long id)
        {
            FilterDefinition<Payment> filter = Builders<Payment>.Filter.Eq(m => m.Id, id);
            return _context
                    .Payments
                    .Find(filter)
                    .FirstOrDefault();
        }
        public void Create(Payment payment)
        {
            _context.Payments.InsertOne(payment);
        }
        public bool Update(Payment payments)
        {
            ReplaceOneResult updateResult =
                 _context
                        .Payments
                        .ReplaceOne(
                            filter: g => g.Id == payments.Id,
                            replacement: payments);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public bool Delete(long id)
        {
            FilterDefinition<Payment> filter = Builders<Payment>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = _context
                                              .Payments
                                              .DeleteOne(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public long GetNextId()
        {
            return _context.Payments.CountDocuments(new BsonDocument()) + 1;
        }
        public Payment GetPaymentByTicket(long id)
        {
            FilterDefinition<Payment> filter = Builders<Payment>.Filter.Eq(m => m.Ticket.Id, id);
            return _context
                    .Payments
                    .Find(filter)
                    .FirstOrDefault();
        }

        public IEnumerable<Payment> GetPaymentsByDate(DateTime initialDate, DateTime finalDate)
        {
            return _context
                    .Payments
                    .Find(p => p.PaidDate >= initialDate && p.PaidDate <= finalDate)
                    .ToList();
        }
    }
}