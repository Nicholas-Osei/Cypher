using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class PlayerFriend : AuditableEntity
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int FriendId { get; set; }
        public Player Friend { get; set; }
    }
}
