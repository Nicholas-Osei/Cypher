using Cypher.Domain.Entities.Cypher;
using System.Collections.Generic;

namespace Cypher.Application.Features.Players.Queries.GetById
{
    public class GetPlayerByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Player> Friends { get; set; }

        public virtual Inventory Inventory { get; set; }
        public float Balance { get; set; }

    }
}