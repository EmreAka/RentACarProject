using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelsController : Controller
    {
        private readonly IFuelService _fuelService;

        public FuelsController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }

        [HttpPost("add")]
        public IActionResult Add(Fuel fuel)
        {
            var result = _fuelService.Add(fuel);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Fuel fuel)
        {
            var result = _fuelService.Delete(fuel);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Fuel fuel)
        {
            var result = _fuelService.Update(fuel);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _fuelService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _fuelService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}