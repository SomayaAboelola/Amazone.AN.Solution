using Amazone.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Services.Contract
{
    public interface IOrderServiece
    {
        Task<Order?> CreateOrderAsync(
            string basketId, int deliveryMethod, string BuyerEmail, Address ShippingAddress);
        Task<IReadOnlyList<Order>>GetOrderForUser(string BuyerEmail);
        Task<Order> GetOrderForUserById (int orderId , string BuyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod();   
    }
}
