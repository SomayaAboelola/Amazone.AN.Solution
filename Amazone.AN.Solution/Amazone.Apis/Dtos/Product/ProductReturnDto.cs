using Amazone.Core.Entities;

namespace Amazone.Apis.Dtos.Product
{
    public class ProductReturnDto : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }

    }
}
