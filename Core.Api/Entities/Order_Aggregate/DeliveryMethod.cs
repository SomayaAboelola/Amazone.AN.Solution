﻿namespace Amazone.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod :BaseEntity<int>
    {
        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public decimal Price { get; set; }
    }
}