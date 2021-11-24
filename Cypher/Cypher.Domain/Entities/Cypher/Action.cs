using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Action : AuditableEntity
    {
        public string Name { get; set; }
        public string Trigger { get; set; }
        public string Info { get; set; }
        public Puzzle Puzzle { get; set; }
    }
}
