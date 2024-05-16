using Amazone.Core.Entities;
using Amazone.Core.Repositories.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core
{
   public interface IUnitOfWork :IAsyncDisposable
    {
        IGenericRepository<TEntity ,TKey> Repository<TEntity, TKey>() where TEntity:BaseEntity<TKey>  ;

        Task<int> CompleteAsync(); 
    }
}
