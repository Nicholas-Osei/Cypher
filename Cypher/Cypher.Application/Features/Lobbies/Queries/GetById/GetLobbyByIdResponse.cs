using Cypher.Domain.Entities.Cypher;
using System.Collections.Generic;

namespace Cypher.Application.Features.Lobbies.Queries.GetById
{
    public class GetLobbyByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Player LobbyAdmin { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}