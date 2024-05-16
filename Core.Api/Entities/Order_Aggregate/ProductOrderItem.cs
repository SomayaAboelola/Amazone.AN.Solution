using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Entities.Order_Aggregate
{
    public class ProductOrderItem
    {
        private ProductOrderItem()
        {
            
        }
        public ProductOrderItem(int productId, string productName, string productPictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPictureUrl = productPictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductPictureUrl { get; set; } = null!;  

        

    }
}
