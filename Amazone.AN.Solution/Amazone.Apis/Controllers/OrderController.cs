using Amazone.Apis.Dtos.Account;
using Amazone.Apis.Dtos.Orders;
using Amazone.Apis.Errors;
using Amazone.Core.Entities.Order_Aggregate;
using Amazone.Core.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{

    public class OrderController : BaseController
    {
        private readonly IOrderServiece _order;
        private readonly IMapper _mapper;

        public OrderController(IOrderServiece order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }


        //[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var result = await _order.CreateOrderAsync(orderDto.BasketId, orderDto.DeliveryMethod, orderDto.BuyerEmail, address);
            if (result is null)
                return BadRequest(new ResponseApi(400));

            return Ok(result);
        }
    }
}