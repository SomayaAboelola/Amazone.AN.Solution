using Amazone.Apis.Errors;
using Amazone.Core.Entities.Basket;
using Amazone.Core.Entities.Order_Aggregate;
using Amazone.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Amazone.Apis.Controllers
{

    public class PaymentController : BaseController
    {

        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        private const string whSecret = "whsec_4bc81e030d6063e2c5abdad4c7e5240e745116b923521aedd2d537668f6b5bd3";

        private readonly IPaymentServices _paymentServices;
        private readonly ILogger<Order> _logger;

        public PaymentController(IPaymentServices paymentServices, ILogger<Order> logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
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

        [Route("webhook")]
        [HttpPost]
        public async Task<IActionResult> webHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], whSecret, 300, false);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            Order? order;
            // Handle the event
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await _paymentServices.UpdateOrderStatus(paymentIntent.Id, true);

                    _logger.LogInformation("Order is Succeeded  {0}", order?.PaymentIntentId);
                    _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                    break;
                case Events.PaymentIntentPaymentFailed:
                    order = await _paymentServices.UpdateOrderStatus(paymentIntent.Id, false);

                    _logger.LogInformation("Order is Failed {0}", order?.PaymentIntentId);
                    _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);

                    break;
            }

            return Ok();



        }
    }
}
