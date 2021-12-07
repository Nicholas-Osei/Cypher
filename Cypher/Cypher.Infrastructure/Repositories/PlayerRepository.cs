using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache _distributedCache;

        public PlayerRepository(IRepositoryAsync<Player> repository, IDistributedCache distributedCache)
        {
            _repo = repository;
            _distributedCache = distributedCache;
        }

        public IQueryable<Player> Players => _repo.Entities;

        public async Task DeleteAsync(Player player)
        {
            await _repo.DeleteAsync(player);
            await _distributedCache.RemoveAsync(CacheKeys.PlayerCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PlayerCacheKeys.GetKey(player.Id));
        }

        public async Task RemoveFriendAsync(Player player, Player friend)
        {
            //foreach (var pf in player.Friends.ToList())
            //{
            //    if (pf.Friend == friend)
            //    {
            //        //player.Friends.Remove(pf);
            //        await _repo.DeleteAsync(-f)
            //    }
            //}
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            return await _repo.Entities
                .Where(p => p.Id == playerId)
                .Include(p => p.Friends)
                //.ThenInclude(pf => pf.Friend)
                .Include(i => i.Inventory)
                .ThenInclude(e => e.Items)
                .FirstOrDefaultAsync();
            
        }

        public async Task<List<Player>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Player player)
        {
            await _repo.AddAsync(player);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            return player.Id;
        }

        public async Task UpdateAsync(Player player)
        {
            await _repo.UpdateAsync(player);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(player.Id));
        }
    }
}
