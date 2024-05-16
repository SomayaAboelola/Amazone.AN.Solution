using Amazone.Core.Entities;
using Amazone.Core.Entities.Data;
using Amazone.Core.Repositories.Contract;
using Amazone.Core.Specification;
using Amazone.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Amazone.Repository
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
          _context.Add(entity);
        }

        public void Delete(TEntity entity)
        {
           _context.Remove(entity); 
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IReadOnlyList<TEntity>)await _context.Set<Product>()
                    .Include(p => p.Brand)
                    .Include(p => p.Type)
                    .ToListAsync();


            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllwihSpecAsync(ISpecisfication<TEntity, Tkey> spec)
        {
            return await ApplyQuery(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Tkey? id)
        {
            //if (typeof(TEntity) == typeof(Product))
            //{
            //    return await _context.Set<Product>()
            //        .Include(p => p.Brand)
            //        .Include(p => p.Type)
            //        .FirstOrDefaultAsync(p=>p.Id==id) as TEntity;


            //}
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> GetCountSpec(ISpecisfication<TEntity, Tkey> spec)
        {
            return await ApplyQuery(spec).CountAsync(); 
        }

        public async Task<TEntity> GetwihSpecAsync(ISpecisfication<TEntity, Tkey> spec)
        {
            return await ApplyQuery(spec).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity); 
        }

        private IQueryable<TEntity> ApplyQuery(ISpecisfication<TEntity, Tkey> spec)
        {
            return  SpecificationEvelouter<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), spec);
        }
    }
}
