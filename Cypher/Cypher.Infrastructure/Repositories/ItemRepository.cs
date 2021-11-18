using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
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

        public async Task<int> InsertAsync(Item item)
        {
            await _repo.AddAsync(item);

            return item.Id;
        }
    }
}
