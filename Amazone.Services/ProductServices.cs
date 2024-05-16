using Amazone.Core;
using Amazone.Core.Entities.Data;
using Amazone.Core.Services.Contract;
using Amazone.Core.Specification.ProductSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Product?>> GetProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpesification(productSpec);
            var products = await _unitOfWork.Repository<Product ,int>().GetAllwihSpecAsync(spec);
            return products; 
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var spec = new ProductSpesification(productId);
            var product = await _unitOfWork.Repository<Product, int>().GetwihSpecAsync(spec);
            return product;
        } 
        
        public Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
           var brand= _unitOfWork.Repository<ProductBrand,int>().GetAllAsync(); 
            return brand;   
        }

        public Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
            var type = _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return type;
        }

        public async Task<int> GetCountAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductWithFiltreationforCountSpecfication(productSpec);

            var count = await _unitOfWork.Repository<Product,int>().GetCountSpec(spec);
            return count;
        }
    }
}
