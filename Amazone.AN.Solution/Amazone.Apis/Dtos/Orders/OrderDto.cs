using System.ComponentModel.DataAnnotations;

namespace Amazone.Apis.Dtos.Orders
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }



    }
}