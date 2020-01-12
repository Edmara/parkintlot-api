using ParkingLotApi.Models;
using System.Collections.Generic;

namespace ParkingLotApi.Repositories
{

    public interface ILotRepository
    {
        IEnumerable<Lot> GetAllLots();
        Lot GetLot(long id);
        bool Update(Lot lot);
        bool Delete(long id);
        long GetNextId();
        void Create(Lot lot);
        IEnumerable<Lot> getEmptiesLots();
        Lot getEmptyLot();
    }
}