using ParkingLotApi.Models;
using System.Collections.Generic;

namespace ParkingLotApi.Services
{
    public interface ILotService
    {

        IEnumerable<Lot> GetAllLots();
        Lot GetLot(long id);
        bool Update(Lot lot);
        bool Delete(long id);
        long GetNextId();
        Lot Create(Lot lot);
        IEnumerable<Lot> getEmptiesLots();
        Lot Entry();
        void Exit(Lot lot);
    }
}
