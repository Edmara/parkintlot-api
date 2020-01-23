using ParkingLotApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TicketsController : Controller
    {
        private readonly ITicketService _service;
        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        // [Authorize("Bearer")]
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            return new ObjectResult(_service.GetAllTickets());
        }

        // [Authorize("Bearer")]
        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(long id)
        {
            var ticket = _service.GetTicket(id);
            if (ticket == null)
                return new NotFoundResult();

            return new ObjectResult(ticket);
        }

        // [Authorize("Bearer")]
        [HttpPut("{id}")]
        public ActionResult<Ticket> Put(long id, [FromBody] Ticket ticket)
        {
            var lotFromDb = _service.GetTicket(id);
            if (lotFromDb == null)
                return new NotFoundResult();
            
            ticket.Id = lotFromDb.Id;
            ticket.InternalId = lotFromDb.InternalId;
            
            return new OkObjectResult(_service.Update(ticket));
        }

    }
}