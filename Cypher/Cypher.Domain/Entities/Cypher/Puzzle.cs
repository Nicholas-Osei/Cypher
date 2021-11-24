using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Puzzle : AuditableEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get { return StartTime.Add(Duration); } }
        public virtual ICollection<Action> Actions { get; set; }
    }
}
