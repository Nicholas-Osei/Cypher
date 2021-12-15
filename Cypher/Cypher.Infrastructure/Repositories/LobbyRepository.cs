using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Infrastructure.Repositories
{
    public class LobbyRepository : ILobbyRepository
    {
        public IQueryable<Lobby> Lobbies => throw new NotImplementedException();

        public Task DeleteAsync(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public Task<Lobby> GetByIdAsync(int lobbyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lobby>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Lobby lobby)
        {
            throw new NotImplementedException();
        }
    }
}
