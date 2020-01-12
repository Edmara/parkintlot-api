using ParkingLotApi.Models;
using System;
using System.Collections.Generic;

namespace ParkingLotApi.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        Payment GetPayment(long id);
        void Create(Payment payment);
        bool Update(Payment payment);
        bool Delete(long id);
        long GetNextId();
        Payment GetPaymentByTicket(long id);
        IEnumerable<Payment> GetPaymentsByDate(DateTime initialDate, DateTime finalDate);
    }
}