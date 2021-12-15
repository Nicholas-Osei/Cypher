using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.Queries
{
    public class GetAllLobbiesResponse
    {
        public int Id { get; set; }
        public Player LobbyAdmin { get; set; }
        public ICollection<PlayerLobby> PlayerLobbies { get; set; }
    }
}
