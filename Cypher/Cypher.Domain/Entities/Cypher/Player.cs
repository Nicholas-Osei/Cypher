using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Player : AuditableEntity
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<MessagePlayer> MessagePlayers { get; set; }
        public virtual ICollection<PlayerLobby> PlayerLobbies { get; set; }
        public virtual ICollection<PlayerFriend> Friends { get; set; }
        public virtual ICollection<PlayerFriend> Players { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
