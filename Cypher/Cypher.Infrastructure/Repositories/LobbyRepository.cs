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
    public class LobbyRepository : ILobbyRepository
    {
        private readonly IRepositoryAsync<Lobby> _repo;

        public IQueryable<Lobby> Lobbies => _repo.Entities;

        public LobbyRepository(IRepositoryAsync<Lobby> repo)
        {
            _repo = repo;
        }


        public Task DeleteAsync(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Lobby>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public Task<Lobby> GetByIdAsync(int lobbyId)
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
