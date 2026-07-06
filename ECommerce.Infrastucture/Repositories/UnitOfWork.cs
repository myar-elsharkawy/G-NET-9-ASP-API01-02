using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastucture.Repositories
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Repository for product
            var TypeName = typeof(TEntity).Name;

            if (repositories.TryGetValue(TypeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;
            else { 
                var repo = new GenericRepository<TEntity, TKey>(dbContext);
                repositories[TypeName] = repo;
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await dbContext.SaveChangesAsync(ct);


    }
}
