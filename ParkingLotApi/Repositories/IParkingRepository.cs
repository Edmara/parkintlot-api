using ParkingLotApi.Models;
using System.Collections.Generic;

namespace ParkingLotApi.Repositories
{
    public interface IParkingRepository
    {
        IEnumerable<Parking> GetAllParkings();
        Parking GetParking(long id);
        void Create(Parking parking);
        bool Update(Parking parking);
        bool Delete(long id);
        long GetNextId();
        IEnumerable<Parking> GetActualParkings();
    }
}