using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Interfaces.CacheRepositories
{
    public interface IPlayerCacheRepository
    {
        Task<List<Player>> GetCacheListAsync();

        Task<Player> GetByIdAsync(int playerId);
    }
}
