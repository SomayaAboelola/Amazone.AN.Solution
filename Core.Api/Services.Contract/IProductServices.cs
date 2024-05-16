using Amazone.Core.Entities.Data;
using Amazone.Core.Specification.ProductSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Services.Contract
{
    public interface IProductServices
    {
        Task<IReadOnlyList<Product?>> GetProductsAsync(ProductSpecParams productSpec);  
      
        Task<Product> GetProductByIdAsync(int productId);

        Task<int> GetCountAsync(ProductSpecParams productSpec);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();

        Task<IReadOnlyList<ProductType>>GetProductTypeAsync();
        
    }
}
