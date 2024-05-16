using Amazone.Core.Entities;
using Amazone.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task <IReadOnlyList<TEntity>> GetAllAsync();    
        Task<TEntity> GetAsync(Tkey id);
        Task <IReadOnlyList<TEntity>> GetAllwihSpecAsync(ISpecisfication<TEntity, Tkey> spec);    
        Task<TEntity> GetwihSpecAsync(ISpecisfication<TEntity, Tkey> spec);

        Task<int> GetCountSpec(ISpecisfication<TEntity, Tkey> spec); 

        void Add (TEntity entity);  
        void Update (TEntity entity);
        void Delete (TEntity entity);   

    }
}
