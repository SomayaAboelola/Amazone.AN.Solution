using Amazone.Core;
using Amazone.Core.Entities.Data;
using Amazone.Core.Entities.Order_Aggregate;
using Amazone.Core.Repositories.Contract;
using Amazone.Core.Services.Contract;
using Amazone.Core.Specification.OderSpec;

namespace Amazone.Services
{
    public class OrderService : IOrderServiece
    {
        private readonly IBasketRepository _basket;
        private readonly IUnitOfWork _unitOfWork;
       

        public OrderService(IBasketRepository basket,
                            IUnitOfWork unitOfWork )

          {
            _basket = basket;
            _unitOfWork = unitOfWork;
           
          }
        public async Task<Order?> CreateOrderAsync(string basketId, int deliveryMethodId, string BuyerEmail, Address ShippingAddress)
        {
            //1.Get BasketId from Basket Services

            var basket = await _basket.GetBasketAsync(basketId);

            //2.Get Selected Item at basket from ProductRepo

            var OrderItems = new List<OrderItem>();
            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
                    var ProductItemOrder = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);

                    OrderItems.Add(orderItem);
                }
            }


            //3.Calculate SubTotal

            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            //4.Get DeliveryMethod from DeliveryMethodRepo
            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(deliveryMethodId);

            //5.Create Oder 

            var order = new Order(
                buyerEmail: BuyerEmail,
                shippingAddress: ShippingAddress,
                deliveryMethod: DeliveryMethod,
                items: OrderItems,
                subtotal: subTotal);

            _unitOfWork.Repository<Order, int>().Add(order);

            //6.Save Database 
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;

            return order;

        }



        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod()
        {
            var result = await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();
            return result;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUser(string BuyerEmail)
        {
           var orderRepo = _unitOfWork.Repository<Order, int>();

            var spec = new OrderSpecification(BuyerEmail);

            var orders = await orderRepo.GetAllwihSpecAsync(spec);
         
            return orders;  
           
        }

        public Task<Order> GetOrderForUserById(int orderId, string BuyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order, int>();
            var spec = new OrderSpecification(orderId, BuyerEmail);

            var order = orderRepo.GetwihSpecAsync(spec);

            return order;   
        }
    }
}
