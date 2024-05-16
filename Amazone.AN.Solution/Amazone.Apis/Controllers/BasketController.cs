using Amazone.Apis.Dtos.Basket;
using Amazone.Apis.Errors;
using Amazone.Core.Entities.Basket;
using Amazone.Core.Repositories.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            return base.Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateorCreateBasket(CustomerBasketDto customerBasket)
        {
            var basketMap = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
            var creatbasket = await _basketRepo.UpdateBasketAsync(basketMap);

            if (creatbasket is null)
                return BadRequest(new ResponseApi(400));

            return Ok(creatbasket);
        }

        [HttpDelete]
        public async Task DeleteAsync(string basketId)
        {
            await _basketRepo.DeleteBasketAsync(basketId);
        }
    }
}
