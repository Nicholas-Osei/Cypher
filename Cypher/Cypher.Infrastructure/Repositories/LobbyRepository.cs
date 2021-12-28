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


        public async Task DeleteAsync(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Lobby>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<Lobby> GetByIdAsync(int lobbyId)
        {
            return await _repo.Entities
                .Where(l => l.Id == lobbyId)
                .Include(l => l.LobbyAdmin)
                .Include(l => l.Players)
                .FirstOrDefaultAsync();
        }

        public async Task<int> InsertAsync(Lobby lobby)
        {
            await _repo.AddAsync(lobby);
            return lobby.Id;
        }

        public async Task UpdateAsync(Lobby lobby)
        {
            throw new NotImplementedException();
        }
    }
}
