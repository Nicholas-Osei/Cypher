using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;

namespace Cypher.Domain.Entities.Cypher
{
    public class Player : AuditableEntity
    {
        public string Name { get; set; }
        //public bool IsAdmin { get; set; }
        public virtual ICollection<MessagePlayer> MessagePlayers { get; set; }
        public virtual ICollection<Lobby> LobbiesAdmin { get; set; }
        public virtual ICollection<Lobby> LobbiesJoined { get; set; }

        [JsonIgnore]
        public virtual ICollection<Player> Friends { get; set; }

        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}