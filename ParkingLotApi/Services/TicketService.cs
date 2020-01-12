using ParkingLotApi.Models;
using ParkingLotApi.Repositories;
using System;
using System.Collections.Generic;

namespace ParkingLotApi.Services
{
    public class TicketService : ITicketService
    {
        private const double FIRSTHREETHOURPRICE = 7;
        private const double HOURPRICE = 3;

        private const double HOURBASE = 3;
        private readonly ITicketRepository _repo;

        private readonly IPaymentService paymentService;

        public TicketService(ITicketRepository repo)
        {
            this._repo = repo;
        }

        public IEnumerable<Ticket> GetAllTickets() => _repo.GetAllTickets();

        public Ticket GetTicket(long id) => _repo.GetTicket(id);

        public long GetNextId() => _repo.GetNextId();

        public bool Delete(long id) => _repo.Delete(id);

        public bool Update(Ticket ticket)
        {
            return _repo.Update(ticket);
        }
        public Ticket Create()
        {
            Guid ticketNumber = Guid.NewGuid();
            Ticket ticket = new Ticket();
            ticket.Id = GetNextId();
            ticket.InitalParkingFee = FIRSTHREETHOURPRICE;
            ticket.FinalParkingFee = null;
            ticket.GeneratedDate = DateTime.Now;
            ticket.Number = ticketNumber;
            _repo.Create(ticket);

            return ticket;
        }

        public bool TicketPayment(Ticket ticket)
        {
            DateTime date = DateTime.Now;
            Payment payment = paymentService.GetPaymentByTicket(ticket);
            if (payment != null
                 && ticket.FinalParkingFee == payment.TotalPaid
                 && date < payment.PaidDate.AddMinutes(5))
            {
                return true;
            }
            return false;
        }

        public void CalculeFinalParkingFee(Ticket ticket)
        {
            DateTime dateNow = DateTime.Now;
            double hours = (ticket.GeneratedDate - dateNow).TotalHours;

            if (hours >= HOURBASE)
            {   
                ticket.FinalParkingFee = (hours * HOURPRICE) + ticket.InitalParkingFee;
                Update(ticket);
            }            

        }
    }
}
