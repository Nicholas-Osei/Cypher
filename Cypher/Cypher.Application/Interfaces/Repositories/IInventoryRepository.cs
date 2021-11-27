using System;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        IQueryable<Inventory> Inventories { get; }
        Task<int> InsertAsync(Inventory inventory);
        Task<Inventory> GetByIdAsync(int inventoryId);
        Task DeleteAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
    }
}
