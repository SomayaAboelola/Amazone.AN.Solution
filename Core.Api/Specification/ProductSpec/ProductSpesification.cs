using Amazone.Core.Entities.Data;
using System;

namespace Amazone.Core.Specification.ProductSpec
{
    public class ProductSpesification : BaseSpecisfication<Product, int>
    {
        public ProductSpesification(ProductSpecParams specParams)
            :
              base(p =>  (string.IsNullOrEmpty(specParams.Search) || p.Name.Contains(specParams.Search)) &&
                         (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
                          (!specParams.TypeId.HasValue ||p.TypeId == specParams.TypeId.Value)
                   )
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type); 

            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
                {
                    case "PriceAsc":
                        OrderBy = o => o.Price;
                        break;
                    case "PriceDesc":
                        OrderByDesc = o => o.Price;
                        break;
                    default:
                        OrderBy = o => o.Name;
                        break;
                }
            }
            else
                OrderBy = o => o.Name;


            ApplyPagination((specParams.PageIndex -1)*specParams.PageSize , specParams.PageSize);       
        }

        public ProductSpesification(int? id) : base(p => p.Id == id)
        {

            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }

    }
}
