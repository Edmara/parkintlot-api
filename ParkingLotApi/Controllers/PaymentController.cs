using ParkingLotApi.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            return new ObjectResult(_service.GetAllPayments());
        }

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public ActionResult<Payment> Get(long id)
        {
            var payment = _service.GetPayment(id);
            if (payment == null)
                return new NotFoundResult();

            return new ObjectResult(payment);
        }

        [Authorize("Bearer")]
        [HttpGet("{initialDate}/{finalDate}")]
        public ActionResult<IEnumerable<Payment>> GetPaymentsByDate(DateTime initialDate, DateTime finalDate)
        {
            var payment = _service.GetPaymentsByDate(initialDate, finalDate);
            if (payment == null)
                return new NotFoundResult();

            return new ObjectResult(payment);
        }

        [Authorize("Bearer")]
        [HttpPost]
        public ActionResult<Payment> Post([FromBody] Ticket ticket)
        {
            _service.Create(ticket);
            var payment = _service.GetPaymentByTicket(ticket);
            return new OkObjectResult(payment);
        }

        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public ActionResult<Payment> Put(long id, [FromBody] Payment payment)
        {
            var lotFromDb = _service.GetPayment(id);
            if (lotFromDb == null)
                return new NotFoundResult();

            payment.Id = lotFromDb.Id;
            payment.InternalId = lotFromDb.InternalId;
            
            return new OkObjectResult(_service.Update(payment));
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var post = _service.GetPayment(id);
            if (post == null)
                return new NotFoundResult();
            _service.Delete(id);
            return new OkResult();
        }
    }
}