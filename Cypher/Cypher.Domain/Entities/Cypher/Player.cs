using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Player : AuditableEntity
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        [JsonIgnore]
        public virtual ICollection<MessagePlayer> MessagePlayers { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlayerLobby> PlayerLobbies { get; set; }
        [JsonIgnore]
        public  Inventory Inventory { get; set; }
    }
}
