using System;
using System.Collections.Generic;
using ParkingLotApi.Models;
using ParkingLotApi.Repositories;

namespace ParkingLotApi.Services
{
    public class ParkingService : IParkingService
    {

        private readonly IParkingRepository _repo;
        private readonly ITicketService ticketService;
        private readonly ILotService lotService;
        private readonly IPaymentService paymentService;

        public ParkingService(IParkingRepository repo, ITicketService ticketService, ILotService lotService, IPaymentService paymentService)
        {
            _repo = repo;
            this.ticketService = ticketService;
            this.lotService = lotService;
            this.paymentService = paymentService;
        }

        public IEnumerable<Parking> GetAllParkings() => _repo.GetAllParkings();
        public Parking GetParking(long id) => _repo.GetParking(id);
        public IEnumerable<Parking> GetActualParkings() => _repo.GetActualParkings();
        public long GetNextId() => _repo.GetNextId();
        public bool Delete(long id) => _repo.Delete(id);
        public bool Update(Parking parking)
        {
            return _repo.Update(parking);
        }
        public Parking Entry()
        {
            Parking parking = new Parking();
            parking.Id = GetNextId();
            parking.EntryDate = new DateTime();
            parking.Ticket = ticketService.Create();
            parking.Lot = lotService.Entry();

            _repo.Create(parking);

            return parking;

        }

        public bool Exit(Parking parking)
        {

            if (ticketService.TicketPayment(parking.Ticket))
            {
                lotService.Exit(parking.Lot);
                parking.ExitDate = DateTime.Now;
                Update(parking);
                return true;
            }
            return false;

        }

    }
}
