using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        IQueryable<Player> Players { get; }
        Task<List<Player>> GetListAsync();
        Task<Player> GetByIdAsync(int playerId);
        Task<int> InsertAsync(Player player);
        Task UpdateAsync(Player player);
        Task DeleteAsync(Player player);
        Task RemoveFriendAsync(Player player, Player friend);
    }
}
