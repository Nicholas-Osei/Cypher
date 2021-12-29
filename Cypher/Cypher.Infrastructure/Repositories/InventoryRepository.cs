using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Cypher.Infrastructure.Repositories
{
    public class InventoryRepository: IInventoryRepository
    {
        private readonly IRepositoryAsync<Inventory> _repo;
        //private readonly IDistributedCache _distributedCache;
        public InventoryRepository(IRepositoryAsync<Inventory> repository)
        {
            _repo = repository;
            //_distributedCache = distributedCache;
        }

        public IQueryable<Inventory> Inventories => _repo.Entities;

        public async Task  DeleteAsync(Inventory inventory)
        {
            await _repo.DeleteAsync(inventory);
            //await _distributedCache.RemoveAsync(CacheKeys..ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.PlayerCacheKeys.GetKey(player.Id));
        }

        public async Task<Inventory> GetByIdAsync(int inventoryId)
        {
            return await _repo.Entities.Include(p=>p.Items).Where(i => i.Id == inventoryId).FirstOrDefaultAsync();
        }

        public async Task<List<Inventory>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Inventory inventory)
        {
            await _repo.AddAsync(inventory);

            return inventory.Id;
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            await _repo.UpdateAsync(inventory);
        }
    }
}
