using Amazone.Apis.Dtos.Orders;
using Amazone.Core.Entities.Order_Aggregate;
using AutoMapper;

namespace Amazone.Apis.Helper
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemPictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductPictureUrl))

                return $"{_config["BaseApiUrl"]}/{source.Product.ProductPictureUrl}";
            return string.Empty;
        }
    }
}
