using ParkingLotApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace ParkingLotApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class LotsController : Controller
    {

        private readonly ILotService _service;
        public LotsController(ILotService service)
        {
           _service = service;
        }

        // [Authorize("Bearer")]
        [HttpGet]
        public ActionResult<IEnumerable<Lot>> Get()
        {
            return new ObjectResult(_service.GetAllLots());
        }

        // [Authorize("Bearer")]
        [HttpGet("EmptyLots")]
        public ActionResult<IEnumerable<Lot>> GetEmptiesLots()
        {
            return new ObjectResult(_service.getEmptiesLots());
        }


        // [Authorize("Bearer")]
        [HttpGet("{id}")]
        public ActionResult<Lot> Get(long id)
        {
           var lot =  _service.GetLot(id);
           if (lot == null)
               return new NotFoundResult();

           return new ObjectResult(lot);
        }

        // [Authorize("Bearer")]
        [HttpPost]
        public ActionResult<Lot> Post([FromBody] Lot lot)
        {
           return new OkObjectResult(_service.Create(lot));
        }

        // [Authorize("Bearer")]
        [HttpPut("{id}")]
        public ActionResult<Lot> Put(long id, [FromBody] Lot lot)
        {

           var lotFromDb =  _service.GetLot(id);
           if (lotFromDb == null)
               return new NotFoundResult();
           
            lot.Id = lotFromDb.Id;
           lot.InternalId = lotFromDb.InternalId;

           return new OkObjectResult(_service.Update(lot));

        }

        // [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
           var post = _service.GetLot(id);
           if (post == null)
               return new NotFoundResult();
           _service.Delete(id);
           return new OkResult();
        }
    }
}