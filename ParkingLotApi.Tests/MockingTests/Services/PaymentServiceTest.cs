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
    public class PaymentServiceTest
    {

        private Mock<IPaymentService> mockedService;

        private readonly int ID_NOT_FOUND_PAYMENT = 0;

        private readonly int ID_FOUND_PAYMENT = 10;

        private readonly Payment CREATE_PAYMENT;

        private readonly Payment CREATED_PAYMENT;

        private readonly Payment UPDATE_PAYMENT;

        private readonly Payment UPDATED_PAYMENT;

        private readonly Ticket ticket;

        public PaymentServiceTest()
        {

            ticket = new Ticket();
            ticket.GeneratedDate = DateTime.Now;
            ticket.InitalParkingFee = 20;
            ticket.FinalParkingFee = 50;

            CREATE_PAYMENT = new Payment();
            CREATE_PAYMENT.PaidDate = DateTime.Now;
            CREATE_PAYMENT.TotalPaid = 100;
            CREATE_PAYMENT.Ticket = ticket;

            CREATED_PAYMENT = new Payment();
            CREATED_PAYMENT.Id = ID_FOUND_PAYMENT;
            CREATED_PAYMENT.PaidDate = CREATE_PAYMENT.PaidDate;
            CREATED_PAYMENT.TotalPaid = 100;
            CREATED_PAYMENT.Ticket = ticket;

            UPDATE_PAYMENT = new Payment();
            UPDATE_PAYMENT.Id = ID_FOUND_PAYMENT;
            UPDATE_PAYMENT.TotalPaid = 110;
            UPDATE_PAYMENT.Ticket = ticket;

            UPDATED_PAYMENT = new Payment();
            UPDATED_PAYMENT.Id = ID_FOUND_PAYMENT;
            UPDATED_PAYMENT.PaidDate = UPDATE_PAYMENT.PaidDate;
            UPDATED_PAYMENT.TotalPaid = UPDATE_PAYMENT.TotalPaid;
            UPDATED_PAYMENT.Ticket = ticket;

            mockedService = new Mock<IPaymentService>();
            mockedService.Setup(m => m.GetAllPayments()).Returns(new List<Payment>());
            mockedService.Setup(m => m.GetPayment(ID_NOT_FOUND_PAYMENT)).Returns((Payment)null);
            mockedService.Setup(m => m.GetPayment(ID_FOUND_PAYMENT)).Returns(CREATED_PAYMENT);
            mockedService.Setup(m => m.Create(ticket)).Returns(CREATED_PAYMENT);
            mockedService.Setup(m => m.Update(UPDATE_PAYMENT)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_FOUND_PAYMENT)).Returns(true);
            mockedService.Setup(m => m.Delete(ID_NOT_FOUND_PAYMENT)).Returns(false);
            mockedService.Setup(m => m.GetNextId()).Returns(ID_FOUND_PAYMENT);
        }

        [TestMethod]
        public void GetEmptyPaymentListTest()
        {
            List<Payment> payments = mockedService.Object.GetAllPayments().ToList();
            Assert.IsTrue(0.Equals(payments.Count));
        }

        [TestMethod]
        public void GetNotFoundPaymentTest()
        {
            Payment payment = mockedService.Object.GetPayment(ID_NOT_FOUND_PAYMENT);
            Assert.IsNull(payment);
        }

        [TestMethod]
        public void GetFoundedPaymentTest()
        {
            Payment payment = mockedService.Object.GetPayment(ID_FOUND_PAYMENT);
            Assert.IsNotNull(payment);
        }

        [TestMethod]
        public void CreatePaymentTest()
        {
            Payment payment = mockedService.Object.Create(ticket);
            Assert.AreEqual(CREATED_PAYMENT, payment);
        }

        [TestMethod]
        public void UpdatePaymentTest()
        {
            bool success = mockedService.Object.Update(UPDATE_PAYMENT);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteExistPaymentTest()
        {
            bool success = mockedService.Object.Delete(ID_FOUND_PAYMENT);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DeleteNotExistPaymentTest()
        {
            bool unsuccess = mockedService.Object.Delete(ID_NOT_FOUND_PAYMENT);
            Assert.IsFalse(unsuccess);
        }

        [TestMethod]
        public void GetNextIdTest()
        {
            long nextID = mockedService.Object.GetNextId();
            Assert.AreEqual(ID_FOUND_PAYMENT, nextID);
        }

    }
}
