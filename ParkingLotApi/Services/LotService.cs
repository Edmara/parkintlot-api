using ParkingLotApi.Models;
using ParkingLotApi.Repositories;
using System.Collections.Generic;

namespace ParkingLotApi.Services
{
    public class LotService : ILotService
    {
        private readonly ILotRepository _repo;

        public LotService()
        {

        }

        public LotService(ILotRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Lot> GetAllLots()
        {
           return _repo.GetAllLots();
        }

        public Lot GetLot(long id)
        {
            return _repo.GetLot(id); 
        }

        public long GetNextId() {
            return _repo.GetNextId(); 
        }

        public bool Delete(long id) 
        {
            return _repo.Delete(id); 
        }

        public bool Update(Lot lot)
        {
            return _repo.Update(lot);
        }
        
        public Lot Create(Lot lot)
        {
            lot.Id = GetNextId();
            lot.FinalDate = null;
            _repo.Create(lot);

            return lot;
        }

        public IEnumerable<Lot> getEmptiesLots()
        {
            return _repo.getEmptiesLots();
        }

        public Lot Entry()
        {
            Lot lot = _repo.getEmptyLot();
            lot.Busy = true;

            return lot;
        }

        internal void Exit(Lot lot)
        {
            lot.Busy = false;
            Update(lot);
        }

    }

}
