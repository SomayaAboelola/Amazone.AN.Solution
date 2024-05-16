using Amazone.Apis.Dtos;
using Amazone.Apis.Dtos.Orders;
using Amazone.Apis.Errors;
using Amazone.Core.Entities.Order_Aggregate;
using Amazone.Core.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class OrdersController : BaseController
    {
        private readonly IOrderServiece _order;
        private readonly IMapper _mapper;


        public OrdersController(IOrderServiece order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        // [Authorize]
        [HttpPost("createOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _order.CreateOrderAsync(orderDto.BasketId, orderDto.DeliveryMethodId, orderDto.BuyerEmail, address);

            if (order is null)
                return BadRequest(new ResponseApi(400));

            return Ok(_mapper.Map<Core.Entities.Order_Aggregate.Order, OrderToReturnDto>(order));
        }


        [HttpGet("GetOrders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser(string email)
        {
            var orders = await _order.GetOrderForUser(email);
            return Ok(_mapper.Map<IReadOnlyList<Core.Entities.Order_Aggregate.Order>, OrderToReturnDto>(orders));
        }




        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        [HttpGet("OrderForOneUser")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUserById(int orderId, string email)
        {

            var order = await _order.GetOrderForUserById(orderId, email);
            return Ok(_mapper.Map<Core.Entities.Order_Aggregate.Order, OrderToReturnDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {

            var result = await _order.GetDeliveryMethod();

            return Ok(result);
        }
    }
}
