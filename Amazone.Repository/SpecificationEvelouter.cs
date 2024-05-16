using Amazone.Core.Entities;
using Amazone.Core.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Repository
{
    public static class SpecificationEvelouter<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public static IQueryable<TEntity>GetQuery (IQueryable<TEntity> inputquery ,ISpecisfication<TEntity, Tkey> spec)
        {
            var query = inputquery; 

            if (spec.Cretiria is not null)
                query= query.Where(spec.Cretiria);

            if (spec.OrderBy is not null)   
                query= query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc is not null)  
                query=query.OrderByDescending(spec.OrderByDesc);    

            if (spec.IsPaginationEnable)
                query= query.Skip(spec.Skip).Take(spec.Take);

            query=spec.Includes.Aggregate(query,(current ,includeExpression)=>current.Include(includeExpression));  

            return query;
        }
    }
}
