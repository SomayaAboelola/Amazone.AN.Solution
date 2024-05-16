using Amazone.Core.Entities.Order_Aggregate;

namespace Amazone.Apis.Dtos.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }
        public string DeliveryMethodCost { get; set; }

        public string orderStatus { get; set; }

        public DateTimeOffset OrderDate { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
