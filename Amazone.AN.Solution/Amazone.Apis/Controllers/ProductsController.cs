using Amazone.Apis.Dtos.Product;
using Amazone.Apis.Errors;
using Amazone.Apis.Helper;
using Amazone.Core.Entities.Data;
using Amazone.Core.Services.Contract;
using Amazone.Core.Specification.ProductSpec;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{

    public class ProductsController : BaseController
    {


        //private readonly IGenericRepository<Product, int> _repository;
        //private readonly IGenericRepository<ProductBrand, int> _repo_Brand;
        //private readonly IGenericRepository<ProductType, int> _repo_Type;
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;

        public ProductsController(
                       ////IGenericRepository<Product, int> repository
                       ////, IGenericRepository<ProductBrand, int> repo_brand
                       ////, IGenericRepository<ProductType, int> repo_type
                       IProductServices productServices
                     , IMapper mapper
                                  )
        {
            //_repository = repository;
            //_repo_Brand = repo_brand;
            //_repo_Type = repo_type;
            _productServices = productServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnDto>>> GetAllAsync([FromQuery] ProductSpecParams specParams)
        {
            // var products = await repository.GetAllAsync();

            var products = await _productServices.GetProductsAsync(specParams);

            var count = await _productServices.GetCountAsync(specParams);

            var data = _mapper.Map<IReadOnlyList<ProductReturnDto>>(products);

            return Ok(new Pagination<ProductReturnDto>(specParams.PageSize, specParams.PageIndex, count, data));

        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReturnDto>> GetProductById(int id)
        {
            // var product = await repository.GetAsync(id);
            var spec = new ProductSpesification(id);
            var product = await _productServices.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductReturnDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _productServices.GetProductBrandAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _productServices.GetProductTypeAsync();
            return Ok(types);
        }

    }
}
