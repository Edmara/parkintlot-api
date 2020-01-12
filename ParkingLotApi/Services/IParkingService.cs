using ParkingLotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public interface IParkingService
    {
        IEnumerable<Parking> GetAllParkings();
        Parking GetParking(long id);
        IEnumerable<Parking> GetActualParkings();
        Parking Entry();
        bool Update(Parking parking);
        bool Delete(long id);
        long GetNextId();
        bool Exit(Parking parking);
    }
}
