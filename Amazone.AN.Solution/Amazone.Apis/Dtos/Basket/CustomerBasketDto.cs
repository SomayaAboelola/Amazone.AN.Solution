using Amazone.Core.Entities.Basket;
using System.ComponentModel.DataAnnotations;

namespace Amazone.Apis.Dtos.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = null!;
        public List<BasketItemDto> Items { get; set; }
    }
}
