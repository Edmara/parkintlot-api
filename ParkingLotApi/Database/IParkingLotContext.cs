using MongoDB.Driver;
using ParkingLotApi.Models;

namespace ParkingLotApi.Database
{

    public interface IParkingLotContext
    {
        IMongoCollection<Lot> Lots { get; }
        IMongoCollection<Payment> Payments { get; }
        IMongoCollection<Parking> Parkings { get; }
        IMongoCollection<Ticket> Tickets { get; }

    }
}