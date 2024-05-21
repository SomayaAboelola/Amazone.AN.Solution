using Amazone.Core.Entities.Basket;
using Amazone.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Services.Contract
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CrateOrUpdatePaymentIntent(string basketId);
        Task<Order?> UpdateOrderStatus(string paymentIntentId ,bool isPaid);
    }
}
