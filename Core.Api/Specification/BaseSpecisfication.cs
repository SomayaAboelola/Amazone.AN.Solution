﻿using Amazone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Specification
{
    public class BaseSpecisfication<TEntity, Tkey> : ISpecisfication<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecisfication()
        {
            
        } 
        public BaseSpecisfication(Expression<Func<TEntity ,bool>> critria)
        {
            Cretiria=critria;   
        }

        public Expression<Func<TEntity,bool>> Cretiria { get ; set ; }
        public List<Expression<Func<TEntity ,Object>>> Includes { get; set; } = new List<Expression<Func<TEntity ,object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get ; set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get ; set ; }
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPaginationEnable { get ; set ; }

        public void ApplyPagination (int skip ,int take)
        {
            IsPaginationEnable = true;  
            Skip = skip;
            Take = take;    
        }

      
    }
}
