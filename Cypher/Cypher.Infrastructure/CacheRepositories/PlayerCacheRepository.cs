using AspNetCoreHero.ThrowR;
using Cypher.Application.Interfaces.CacheRepositories;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Cypher.Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using AspNetCoreHero.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Infrastructure.CacheRepositories
{
    public class PlayerCacheRepository : IPlayerCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPlayerRepository _playerRepository;

        public PlayerCacheRepository(IPlayerRepository playerRepository, IDistributedCache distributedCache)
        {
            _playerRepository = playerRepository;
            _distributedCache = distributedCache;
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            string cacheKey = BrandCacheKeys.GetKey(playerId);
            var player = await _distributedCache.GetAsync<Player>(cacheKey);
            if (player == null)
            {
                player = await _playerRepository.GetByIdAsync(playerId);
                Throw.Exception.IfNull(player, "Player", "No Player Found");
                await _distributedCache.SetAsync(cacheKey, player);
            }
            return player;
        }

        public async Task<List<Player>> GetCacheListAsync()
        {
            string cacheKey = PlayerCacheKeys.ListKey;
            var playerList = await _distributedCache.GetAsync<List<Player>>(cacheKey);
            if (playerList == null)
            {
                playerList = await _playerRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, playerList);
            }
            return playerList;
        }
    }
}
