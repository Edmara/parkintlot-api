using ParkingLotApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace ParkingLotApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ParkingsController : Controller
    {
        private readonly IParkingService _service;

        public ParkingsController(IParkingService service)
        {
            _service = service;
        }

        // [Authorize("Bearer")]
        [HttpGet]
        public ActionResult<IEnumerable<Parking>> Get()
        {
            return new ObjectResult(_service.GetAllParkings());

        }

        // [Authorize("Bearer")]
        [HttpGet("ActualParkings")]
        public ActionResult<IEnumerable<Parking>> GetActualParkings()
        {
            return new ObjectResult(_service.GetActualParkings());
        }

        // [Authorize("Bearer")]
        [HttpGet("{id}")]
        public ActionResult<Parking> Get(long id)
        {
            var parking =  _service.GetParking(id);
            if (parking == null)
                return new NotFoundResult();

            return new ObjectResult(parking);
        }

        // [Authorize("Bearer")]
        [HttpPost]
        public ActionResult<Parking> Post()
        {
            return new OkObjectResult(_service.Entry());
        }

        // [Authorize("Bearer")]
        [HttpPut("{id}")]
        public ActionResult<Parking> Put(long id, [FromBody] Parking parking)
        {
            var parkingFromDb =  _service.GetParking(id);
            if (parkingFromDb == null)
                return new NotFoundResult();
            parking.Id = parkingFromDb.Id;
            parking.InternalId = parkingFromDb.InternalId;
            return new OkObjectResult(_service.Update(parking));
        }

        // [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var post =  _service.GetParking(id);
            if (post == null)
                return new NotFoundResult();
             _service.Delete(id);
            return new OkResult();
        }
    }
}