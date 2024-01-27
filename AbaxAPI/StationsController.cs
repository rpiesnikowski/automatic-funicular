using AbaxService.StationService;
using AbaxService.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AbaxAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly ITrainInputValidator validator;
        private readonly IStationService stationService;

        public StationsController(
            ITrainInputValidator validator, 
            IStationService stationService)
        {
            this.validator = validator;
            this.stationService = stationService;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{input}")]
        public ActionResult Get(string input)
        {
            var validationResult = validator.Validate(input);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ValidationMessage);
            }

            var serviceResult = stationService.Get(input);
            // TODO: Mapper.Map<RestStationResult>(serviceResult) ?
            return new JsonResult(serviceResult);
        }
    }
}
