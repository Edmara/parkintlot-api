using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkingLotApi.Models;
using ParkingLotApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApi.Tests.MockingTests.Services
{
    [TestClass]
    public class ParkingServiceTest
    {

        private Mock<IParkingService> mockedService;

        private readonly int ID_NOT_FOUND_PARKING = 0;

        private readonly int ID_FOUND_PARKING = 10;

        private readonly Parking CREATE_PARKING;

        private readonly Parking CREATED_PARKING;

        private readonly Parking UPDATE_PARKING;

        private readonly Parking UPDATED_PARKING;

        public ParkingServiceTest()
        {

            CREATE_PARKING = new Parking();
            CREATE_PARKING.EntryDate = DateTime.Now;
            /*CREATE_PARKING.Ticket = mockTicket;
            CREATE_PARKING.Lot = mockLot;*/

            CREATED_PARKING = new Parking();
            CREATED_PARKING.Id = ID_FOUND_PARKING;
            CREATED_PARKING.EntryDate = CREATE_PARKING.EntryDate;
            /*CREATED_PARKING.Ticket = mockTicket;
            CREATED_PARKING.Lot = mockLot;*/

            UPDATE_PARKING = new Parking();
            UPDATE_PARKING.Id = ID_FOUND_PARKING;
            UPDATE_PARKING.ExitDate = CREATE_PARKING.EntryDate.AddHours(10);

            UPDATED_PARKING = new Parking();
            UPDATED_PARKING.Id = ID_FOUND_PARKING;
            UPDATED_PARKING.EntryDate = CREATE_PARKING.EntryDate;
            UPDATED_PARKING.ExitDate = UPDATE_PARKING.ExitDate;
            /*UPDATED_PARKING.Ticket = mockTicket;
            UPDATED_PARKING.Lot = mockLot;*/

            mockedService = new Mock<IParkingService>();
            mockedService.Setup(m => m.GetAllParkings()).Returns(new List<Parking>());
            mockedService.Setup(m => m.GetParking(ID_NOT_FOUND_PARKING)).Returns((Parking)null);
            mockedService.Setup(m => m.GetParking(ID_FOUND_PARKING)).Returns(CREATED_PARKING);
            mockedService.Setup(m => m.Entry()).Returns(CREATED_PARKING);
            mockedService.Setup(m => m.Update(UPDATE_PARKING)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_FOUND_PARKING)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_NOT_FOUND_PARKING)).Returns(false);
            mockedService.Setup(m => m.GetNextId()).Returns(ID_FOUND_PARKING);
        }

        [TestMethod]
        public void GetEmptyParkingListTest()
        {
            List<Parking> lots = mockedService.Object.GetAllParkings().ToList();
            Assert.IsTrue(0.Equals(lots.Count));
        }

        [TestMethod]
        public void GetNotFoundParkingTest()
        {
            Parking lot = mockedService.Object.GetParking(ID_NOT_FOUND_PARKING);
            Assert.IsNull(lot);
        }

        [TestMethod]
        public void GetFoundedParkingTest()
        {
            Parking lot = mockedService.Object.GetParking(ID_FOUND_PARKING);
            Assert.IsNotNull(lot);
        }

        [TestMethod]
        public void CreateParkingTest()
        {
            Parking lot = mockedService.Object.Entry();
            Assert.AreEqual(CREATED_PARKING, lot);
        }

        [TestMethod]
        public void UpdateParkingTest()
        {
            bool success = mockedService.Object.Update(UPDATE_PARKING);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteExistParkingTest()
        {
            bool success = mockedService.Object.Delete(ID_FOUND_PARKING);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteNotExistParkingTest()
        {
            bool unsuccess = mockedService.Object.Delete(ID_NOT_FOUND_PARKING);
            Assert.IsFalse(unsuccess);
        }

        [TestMethod]
        public void GetNextIdTest()
        {
            long nextID = mockedService.Object.GetNextId();
            Assert.AreEqual(ID_FOUND_PARKING, nextID);
        }

    }
}
