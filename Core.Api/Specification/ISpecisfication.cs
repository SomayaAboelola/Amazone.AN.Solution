using Amazone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Specification
{
    public interface ISpecisfication<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Expression<Func<TEntity ,bool>> Cretiria { get; set; }
        List<Expression<Func<TEntity ,object>>> Includes { get; set; }
       public Expression<Func<TEntity ,object>> OrderBy { get; set; } 
      public Expression<Func<TEntity ,object>> OrderByDesc { get; set; } 
      public int Skip {  get; set; }    
      public int Take {  get; set; }
      public bool IsPaginationEnable { get; set; }
 


    }
}
