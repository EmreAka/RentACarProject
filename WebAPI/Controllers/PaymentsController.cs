using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pay")]
        public IActionResult Pay(Card card)
        {
            var result = _paymentService.Pay(card);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}