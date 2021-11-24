using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IItemRepository
    {
        IQueryable<Item> Items { get; }
        Task<int> InsertAsync(Item item);
        Task<Item> GetByIdAsync(int itemId);
        Task DeleteAsync(Item item);
        Task UpdateAsync(Item item);
    }
}
