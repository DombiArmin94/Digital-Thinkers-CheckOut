using Checkout.Service;
using Microsoft.AspNetCore.Mvc;

namespace CheckOut.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[action]")]
    public class CheckoutV1Controller : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMoneyStockService _iMoneyStockService;

        public CheckoutV1Controller(IMoneyStockService iMoneyStockService, ILogger<CheckoutV1Controller> logger)
        {
            _logger = logger;
            _iMoneyStockService = iMoneyStockService;
        }

        [HttpGet]
        public async Task<IActionResult> Stock()
        {
            var stock = await _iMoneyStockService.GetStockAsync();

            return Ok(stock);
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
