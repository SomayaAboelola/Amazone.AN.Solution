using Amazone.Apis.Dtos;
using Amazone.Apis.Dtos.Basket;
using Amazone.Apis.Dtos.Orders;
using Amazone.Apis.Dtos.Product;
using Amazone.Core.Entities.Basket;
using Amazone.Core.Entities.Data;
using Amazone.Core.Entities.Order_Aggregate;
using AutoMapper;

namespace Amazone.Apis.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, Address>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
              .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
              .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
               .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
               .ForMember(d => d.ProductPictureUrl, o => o.MapFrom(s => s.Product.ProductPictureUrl))
               .ForMember(d => d.ProductPictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();
        }
    }
}
