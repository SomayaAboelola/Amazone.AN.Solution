using Amazone.Core;
using Amazone.Core.Entities.Basket;
using Amazone.Core.Repositories.Contract;
using Amazone.Core.Services.Contract;
using Amazone.Core.Entities.Data;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Amazone.Core.Entities.Data.Product;
using Amazone.Core.Entities.Order_Aggregate;

namespace Amazone.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServices(IConfiguration configuration,
                               IBasketRepository basketRepo,
                               IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CrateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:Secretkey"];
            var productRepo = _unitOfWork.Repository<Product, int>();
            var basket =await _basketRepo.GetBasketAsync(basketId);
            if (basket == null) return null;

            var shippingprice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod ,int>().GetAsync(basket.DeliveryMethodId.Value);
                shippingprice= deliveryMethod.Price;
            }
            
            if(basket.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                  
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            PaymentIntent paymentIntent;
           PaymentIntentService  paymentIntentService = new PaymentIntentService();

            if(string.IsNullOrEmpty(basket.PaymentIntentId))  // Create New Payment Intent 
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingprice * 100,
                    Currency = "usd",
                    PaymentMethodTypes=new List<string> {"card"}

                };

                paymentIntent = await paymentIntentService.CreateAsync(option); // Integration with Stripe
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }

            else  // Update Existing Payment Intent
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingprice * 100,

                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId ,option); 

            }

            await _basketRepo.UpdateBasketAsync(basket); 
            
            return basket;
        }
    }
}
