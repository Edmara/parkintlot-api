using ParkingLotApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Repositories
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicket(long id);
        Task Create(Ticket parking);
        bool Update(Ticket parking);
        bool Delete(long id);
        long GetNextId();
    }
}