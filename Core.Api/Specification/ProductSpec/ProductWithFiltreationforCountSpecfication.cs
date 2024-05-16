using Amazone.Core.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Specification.ProductSpec
{
    public class ProductWithFiltreationforCountSpecfication :BaseSpecisfication<Product ,int>
    {
        public ProductWithFiltreationforCountSpecfication(ProductSpecParams specParams)
            : base(p => (string.IsNullOrEmpty(specParams.Search) || p.Name.Contains(specParams.Search))&&
                        (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
                        (!specParams.TypeId.HasValue ||p.TypeId == specParams.TypeId.Value)
                   )
        {
            
        }
    }
}
