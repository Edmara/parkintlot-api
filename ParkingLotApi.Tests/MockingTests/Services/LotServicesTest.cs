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
    public class LotServicesTest
    {

        private Mock<ILotService> mockedService;

        private readonly int ID_NOT_FOUND_LOT = 0;
        
        private readonly int ID_FOUND_LOT = 10;

        private readonly Lot CREATE_LOT;
        
        private readonly Lot CREATED_LOT;

        private readonly Lot UPDATE_LOT;

        private readonly Lot UPDATED_LOT;

        public LotServicesTest()
        {
            CREATE_LOT = new Lot();
            CREATE_LOT.Name = "LOT_NAME";
            CREATE_LOT.Busy = false;
            CREATE_LOT.InitialDate = DateTime.Now;
            CREATE_LOT.FinalDate = DateTime.Now.AddDays(30);

            CREATED_LOT = new Lot();
            CREATED_LOT.Id = ID_FOUND_LOT;
            CREATED_LOT.Name = "LOT_NAME";
            CREATED_LOT.Busy = false;
            CREATED_LOT.InitialDate = CREATE_LOT.InitialDate;
            CREATED_LOT.FinalDate = CREATE_LOT.FinalDate;

            UPDATE_LOT = new Lot();
            UPDATE_LOT.Id = ID_FOUND_LOT;
            UPDATE_LOT.Name = "LOT_NEW_NAME";

            UPDATED_LOT = new Lot();
            UPDATED_LOT.Id = ID_FOUND_LOT;
            UPDATED_LOT.Name = "LOT_NEW_NAME";
            UPDATED_LOT.Busy = false;
            UPDATED_LOT.InitialDate = CREATE_LOT.InitialDate;
            UPDATED_LOT.FinalDate = CREATE_LOT.FinalDate;

            mockedService = new Mock<ILotService>();
            mockedService.Setup(m => m.GetAllLots()).Returns(new List<Lot>());
            mockedService.Setup(m => m.GetLot(ID_NOT_FOUND_LOT)).Returns((Lot)null);
            mockedService.Setup(m => m.GetLot(ID_FOUND_LOT)).Returns(CREATED_LOT);
            mockedService.Setup(m => m.Create(CREATE_LOT)).Returns(CREATED_LOT);
            mockedService.Setup(m => m.Update(UPDATE_LOT)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_FOUND_LOT)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_NOT_FOUND_LOT)).Returns(false);
            mockedService.Setup(m => m.GetNextId()).Returns(ID_FOUND_LOT);
        }

        [TestMethod]
        public void GetEmptyLotListTest()
        {
            List<Lot> lots = mockedService.Object.GetAllLots().ToList();
            Assert.IsTrue(0.Equals(lots.Count));
        }

        [TestMethod]
        public void GetNotFoundLotTest()
        {
            Lot lot = mockedService.Object.GetLot(ID_NOT_FOUND_LOT);
            Assert.IsNull(lot);
        }

        [TestMethod]
        public void GetFoundedLotTest()
        {
            Lot lot = mockedService.Object.GetLot(ID_FOUND_LOT);
            Assert.IsNotNull(lot);
        }

        [TestMethod]
        public void CreateLotTest()
        {
            Lot lot = mockedService.Object.Create(CREATE_LOT);
            Assert.AreEqual(CREATED_LOT, lot);
        }

        [TestMethod]
        public void UpdateLotTest()
        {
            bool success = mockedService.Object.Update(UPDATE_LOT);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteExistLotTest()
        {
            bool success = mockedService.Object.Delete(ID_FOUND_LOT);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteNotExistLotTest()
        {
            bool unsuccess = mockedService.Object.Delete(ID_NOT_FOUND_LOT);
            Assert.IsFalse(unsuccess);
        }

        [TestMethod]
        public void GetNextIdTest()
        {
            long nextID = mockedService.Object.GetNextId();
            Assert.AreEqual(ID_FOUND_LOT,nextID);
        }

    }
}
