using Checkout.Model.Enums;
using Checkout.Service;
using Checkout.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

            _logger.LogInformation("[Get] Stock returned with model:{Stock}", JsonSerializer.Serialize(stock));

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Stock([FromBody] HungarianForintVM stock)
        {
            if (stock == null || !ModelState.IsValid)
            {
                BadRequest("Invalid parameter!");
            }

            if (stock.IsPositive)
            {
                BadRequest("Cannot stock with negative amounts");
            }

            _logger.LogInformation("[Post] Stock attempting to stock up with model:{Stock}", JsonSerializer.Serialize(stock));

            var result = await _iMoneyStockService.AddToStockAsync(stock);

            if (result)
            {
                _logger.LogInformation("[Post] Stock success");
                return Ok();
            }
            else
            {
                _logger.LogError("[Post] Stock failed to update!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutVM checkout)
        {
            if (checkout == null || !ModelState.IsValid || checkout.InsertedMoney == null)
            {
                BadRequest("Invalid parameter!");
            }

            _logger.LogInformation("[Post] Checkout attempting checkout with model:{Checkout}", JsonSerializer.Serialize(checkout));

            var (change, errorMessage) = await _iMoneyStockService.Checkout(checkout);

            if (change == null)
            {
                _logger.LogWarning("[Post] Checkout failed to checkout with reason:{Checkout}", errorMessage);
                return BadRequest(errorMessage);
            }

            _logger.LogInformation("[Post] Checkout success with Model; {Change}", JsonSerializer.Serialize(change));
            return Ok(change);
        }
    }
}
