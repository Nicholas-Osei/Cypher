using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Lobby : AuditableEntity
    {
        public string Name { get; set; }
        public Player LobbyAdmin { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
