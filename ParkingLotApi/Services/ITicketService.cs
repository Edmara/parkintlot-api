using ParkingLotApi.Models;
using System.Collections.Generic;

namespace ParkingLotApi.Services
{
    public interface ITicketService
    {

        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicket(long id);
        Ticket Create();
        bool Update(Ticket parking);
        bool Delete(long id);
        long GetNextId();
        bool TicketPayment(Ticket ticketId);
        void CalculeFinalParkingFee(Ticket ticket);
    }
}
