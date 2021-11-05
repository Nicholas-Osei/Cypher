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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IRepositoryAsync<Player> _repo;

        public PlayerRepository(IRepositoryAsync<Player> repository)
        {
            _repo = repository;
        }

        public IQueryable<Player> Players => _repo.Entities;

        public async Task DeleteAsync(Player player)
        {
            await _repo.DeleteAsync(player);
        }

        public async Task<Player> GetByIdAsync(int productId)
        {
            return await _repo.Entities.Where(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public Task<List<Player>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
