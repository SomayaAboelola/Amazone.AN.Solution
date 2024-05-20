using Amazone.Apis.Errors;
using Amazone.Core.Entities.Basket;
using Amazone.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentServices _paymentServices;
        public PaymentController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        [Authorize]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        [HttpGet("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentServices.CrateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest(new ResponseApi(400));

            return Ok(basket);

        }

    }
}
