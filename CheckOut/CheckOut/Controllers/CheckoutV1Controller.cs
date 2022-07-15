using Microsoft.AspNetCore.Mvc;

namespace CheckOut.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[action]")]
    public class CheckoutV1Controller : ControllerBase
    {
        protected readonly ILogger _logger;

        public CheckoutV1Controller(ILogger<CheckoutV1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Stock()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Stock(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            return Ok();
        }
    }
}
