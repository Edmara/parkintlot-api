using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ParkingLotApi.Services;
using ParkingLotApi.Models;

namespace ParkingLotApi.Tests.MockingTests.Services
{
    [TestClass]
    public class TicketServiceTest
    {

        private Mock<ITicketService> mockedService;

        private readonly int ID_NOT_FOUND_TICKET = 0;

        private readonly int ID_FOUND_TICKET = 10;

        private readonly Ticket CREATE_TICKET;

        private readonly Ticket CREATED_TICKET;

        private readonly Ticket UPDATE_TICKET;

        private readonly Ticket UPDATED_TICKET;

        public TicketServiceTest()
        {

            CREATE_TICKET = new Ticket();
            CREATE_TICKET.GeneratedDate = DateTime.Now;
            CREATE_TICKET.InitalParkingFee = 20;
            CREATE_TICKET.FinalParkingFee = 50;

            CREATED_TICKET = new Ticket();
            CREATED_TICKET.Id = ID_FOUND_TICKET;
            CREATED_TICKET.GeneratedDate = CREATE_TICKET.GeneratedDate;
            CREATED_TICKET.InitalParkingFee = CREATE_TICKET.InitalParkingFee;
            CREATED_TICKET.FinalParkingFee = CREATE_TICKET.FinalParkingFee;

            UPDATE_TICKET = new Ticket();
            UPDATE_TICKET.Id = ID_FOUND_TICKET;
            UPDATE_TICKET.InitalParkingFee = 30;

            UPDATED_TICKET = new Ticket();
            UPDATED_TICKET.Id = ID_FOUND_TICKET;
            UPDATED_TICKET.GeneratedDate = CREATE_TICKET.GeneratedDate;
            CREATED_TICKET.InitalParkingFee = UPDATE_TICKET.InitalParkingFee;
            UPDATED_TICKET.FinalParkingFee = CREATE_TICKET.FinalParkingFee;

            mockedService = new Mock<ITicketService>();
            mockedService.Setup(m => m.GetAllTickets()).Returns(new List<Ticket>());
            mockedService.Setup(m => m.GetTicket(ID_NOT_FOUND_TICKET)).Returns((Ticket)null);
            mockedService.Setup(m => m.GetTicket(ID_FOUND_TICKET)).Returns(CREATED_TICKET);
            mockedService.Setup(m => m.Create()).Returns(CREATED_TICKET);
            mockedService.Setup(m => m.Update(UPDATE_TICKET)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_FOUND_TICKET)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_NOT_FOUND_TICKET)).Returns(false);
            mockedService.Setup(m => m.GetNextId()).Returns(ID_FOUND_TICKET);

        }

        [TestMethod]
        public void GetEmptyTicketListTest()
        {
            List<Ticket> tickets = mockedService.Object.GetAllTickets().ToList();
            Assert.IsTrue(0.Equals(tickets.Count));
        }

        [TestMethod]
        public void GetNotFoundTicketTest()
        {
            Ticket ticket = mockedService.Object.GetTicket(ID_NOT_FOUND_TICKET);
            Assert.IsNull(ticket);
        }

        [TestMethod]
        public void GetFoundedTicketTest()
        {
            Ticket ticket = mockedService.Object.GetTicket(ID_FOUND_TICKET);
            Assert.IsNotNull(ticket);
        }

        [TestMethod]
        public void CreateTicketTest()
        {
            Ticket ticket = mockedService.Object.Create();
            Assert.AreEqual(CREATED_TICKET, ticket);
        }

        [TestMethod]
        public void UpdateTicketTest()
        {
            bool success = mockedService.Object.Update(UPDATE_TICKET);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteExistTicketTest()
        {
            bool success = mockedService.Object.Delete(ID_FOUND_TICKET);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteNotExistTicketTest()
        {
            bool unsuccess = mockedService.Object.Delete(ID_NOT_FOUND_TICKET);
            Assert.IsFalse(unsuccess);
        }

        [TestMethod]
        public void GetNextIdTest()
        {
            long nextID = mockedService.Object.GetNextId();
            Assert.AreEqual(ID_FOUND_TICKET, nextID);
        }

    }
}
