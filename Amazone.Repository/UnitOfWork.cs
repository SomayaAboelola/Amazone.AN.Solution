using Amazone.Core;
using Amazone.Core.Entities;
using Amazone.Core.Repositories.Contract;
using Amazone.Repository.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repository;
        private readonly StoreDbContext _context;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repository = new Hashtable();   
        }
        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();   

        public async ValueTask DisposeAsync()
        =>await _context.DisposeAsync();  

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var key = typeof(TEntity).Name;

            if (!_repository.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity, TKey>(_context);
                _repository.Add(key, repository);
            }
            return _repository[key] as IGenericRepository<TEntity, TKey>;
          

        }
    }
}
