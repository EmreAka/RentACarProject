using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnginesController : Controller
    {
        private readonly IEngineService _engineService;

        public EnginesController(IEngineService engineService)
        {
            _engineService = engineService;
        }

        [HttpPost("add")]
        public IActionResult Add(Engine engine)
        {
            var result = _engineService.Add(engine);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Engine engine)
        {
            var result = _engineService.Update(engine);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Engine engine)
        {
            var result = _engineService.Delete(engine);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _engineService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _engineService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}