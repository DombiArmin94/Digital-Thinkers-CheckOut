﻿using Checkout.Service;
using Checkout.ViewModels;
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
        public async Task<IActionResult> Stock([FromBody]HungarianForintVM stock)
        {
            if(stock == null || !ModelState.IsValid)
            {
                BadRequest("Invalid parameter!");
            }

            var result = await _iMoneyStockService.AddToStockAsync(stock);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutVM checkout)
        {
            if (checkout == null || !ModelState.IsValid)
            {
                BadRequest("Invalid parameter!");
            }

            var (change, erorrMessage) = await _iMoneyStockService.Checkout(checkout);

            if(change == null)
            {
                return BadRequest(erorrMessage);
            }

            return Ok(change);
        }
    }
}
