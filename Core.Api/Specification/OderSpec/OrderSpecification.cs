using Amazone.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Specification.OderSpec
{
    public class OrderSpecification : BaseSpecisfication<Order, int>
    {
        public OrderSpecification(string email) : base(o=>o.BuyerEmail==email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            OrderByDesc = o => o.OrderDate;
        } 
        
        public OrderSpecification(int orderId ,string email) : base(
            o=>o.Id==orderId && o.BuyerEmail==email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

           
        }


    }
}
