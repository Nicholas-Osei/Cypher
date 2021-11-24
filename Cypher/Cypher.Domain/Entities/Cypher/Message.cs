using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    // Uses AuditableEntity atm, but base prop CreatedBy is not needed because Sender?
    public class Message : AuditableEntity
    {
        public Player Sender { get; set; }
        public virtual ICollection<MessagePlayer> MessagePlayers { get; set; }
        public string MessageText { get; set; }
        public enum Type
        {
            message, whisper, hint
        }
    }
}
