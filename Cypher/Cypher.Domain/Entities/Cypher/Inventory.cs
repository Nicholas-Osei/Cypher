using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Inventory : BaseEntity
    {
        [JsonIgnore]

        public virtual IEnumerable<Item> Items { get; set; }
    }
}
