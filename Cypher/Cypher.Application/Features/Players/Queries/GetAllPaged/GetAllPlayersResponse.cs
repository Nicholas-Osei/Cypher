using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Players.Queries.GetAllPaged
{
    public class GetAllPlayersResponse
    {
        public int Id { get; set; }
        public int check { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public Inventory inventory { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<MessagePlayer> Messages { get; set; }
        public ICollection<PlayerLobby> PlayerLobbies { get; set; }
        
    }
}
