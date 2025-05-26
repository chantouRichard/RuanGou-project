// Controllers/CityController.cs
using Microsoft.AspNetCore.Mvc;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityCodeService _cityService;

        public CityController(ICityCodeService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("adcode")]
        public IActionResult GetAdcode([FromQuery] string city)
        {
            try
            {
                var adcode = _cityService.GetAdcodeByCityName(city);
                return Ok(new { adcode });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}