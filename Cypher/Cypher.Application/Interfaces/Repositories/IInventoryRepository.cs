using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        IQueryable<Inventory> Inventories { get; }
        Task<int> InsertAsync(Inventory inventory);
        Task<List<Inventory>> GetListAsync();
        Task<Inventory> GetByIdAsync(int inventoryId);
        Task DeleteAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
    }
}
