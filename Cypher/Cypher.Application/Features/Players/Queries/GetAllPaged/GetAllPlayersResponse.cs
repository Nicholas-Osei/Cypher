using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;

namespace Cypher.Application.Features.Players.Queries.GetAllPaged
{
    public class GetAllPlayersResponse
    {
        public int Id { get; set; }
        //public int check { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        //public bool IsAdmin { get; set; }
        //public Inventory inventory { get; set; }
        //public ICollection<Item> Items { get; set; } = new List<Item>();
        //public ICollection<MessagePlayer> Messages { get; set; }
        //public ICollection<Lobby> Lobbies { get; set; }

    }
}
