using ParkingLotApi.Models;
using System;
using System.Collections.Generic;

namespace ParkingLotApi.Services
{
    public interface IPaymentService
    {

        IEnumerable<Payment> GetAllPayments();
        Payment GetPayment(long id);
        Payment Create(Ticket ticket);
        bool Update(Payment payment);
        bool Delete(long id);
        long GetNextId();
        Payment GetPaymentByTicket(Ticket ticket);
        IEnumerable<Payment> GetPaymentsByDate(DateTime initialDate, DateTime finalDate);
    }
}
