using ParkingLotApi.Models;
using ParkingLotApi.Repositories;
using System;
using System.Collections.Generic;

namespace ParkingLotApi.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IPaymentRepository _repo;
        private readonly ITicketService ticketService;
        public PaymentService(IPaymentRepository repo)
        {
            this._repo = repo;
        }

        public IEnumerable<Payment> GetAllPayments() => _repo.GetAllPayments();

        public Payment GetPayment(long id) => _repo.GetPayment(id);

        public IEnumerable<Payment> GetPaymentsByDate(DateTime initialDate, DateTime finalDate) => _repo.GetPaymentsByDate(initialDate, finalDate);

        public long GetNextId() => _repo.GetNextId();

        public bool Delete(long id) => _repo.Delete(id);

        public bool Update(Payment payment)
        {
            return _repo.Update(payment);
        }

        // public Payment Create(Payment payment)
        // {
        //     payment.Id = GetNextId();
        //     _repo.Create(payment);

        //     return payment;
        // }

        public Payment Create(Ticket ticket)
        {
            Payment payment = new Payment();
            payment.Ticket = ticket;
            ticketService.CalculeFinalParkingFee(ticket);
            payment.Id = GetNextId();
            _repo.Create(payment);

            return payment;
        }


        public Payment GetPaymentByTicket(Ticket ticket)
        {
            Payment payment = _repo.GetPaymentByTicket(ticket.Id);
            return payment;

        }

    }
}
