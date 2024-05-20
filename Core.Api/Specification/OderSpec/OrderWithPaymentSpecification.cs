using Amazone.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Specification.OderSpec
{
  public class OrderWithPaymentSpecification :BaseSpecisfication<Order ,int>
    {
        public OrderWithPaymentSpecification(string paymentIntentId) :
            base (O=>O.PaymentIntentId==paymentIntentId)    
        {
            
        }
    }
}
