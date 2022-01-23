using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IRepositoryAsync<Item> _repo;

        public ItemRepository(IRepositoryAsync<Item> repo)
        {
            _repo = repo;
        }

        public IQueryable<Item> Items => _repo.Entities;

        public async Task DeleteAsync(Item item)
        {
            await _repo.DeleteAsync(item);
        }

        public async Task<Item> GetByIdAsync(int itemId)
        {
            return await _repo.Entities.Where(i => i.Id == itemId).FirstOrDefaultAsync();
        }

        public async Task<List<Item>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Item item)
        {
            await _repo.AddAsync(item);

            return item.Id;
        }

        public async Task UpdateAsync(Item item)
        {
            await _repo.UpdateAsync(item);
        }
    }
}
