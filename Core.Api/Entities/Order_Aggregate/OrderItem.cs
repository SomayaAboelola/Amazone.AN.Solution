using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Entities.Order_Aggregate
{
    public class OrderItem :BaseEntity<int>
    {
        private OrderItem()
        {
            
        }

        public OrderItem(ProductOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductOrderItem Product { get; set; } = null!;  
     
        public  decimal Price {  get; set; }    
        public int Quantity { get; set; }

    }
}
