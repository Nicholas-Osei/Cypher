using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface ILobbyRepository
    {
        IQueryable<Lobby> Lobbies { get; }
        Task<List<Lobby>> GetListAsync();
        Task<Lobby> GetByIdAsync(int lobbyId);
        Task<int> InsertAsync(Lobby lobby);
        Task UpdateAsync(Lobby lobby);
        Task DeleteAsync(Lobby lobby);
    }
}
