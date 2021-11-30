using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Item : AuditableEntity
    {
        public string Name { get; set; }
        public string ItemType { get; set; }
        [JsonIgnore]

        public virtual Inventory Inventory { get; set; }

    }

    //public enum ItemType
    //{
    //    type1,
    //    type2,
    //    type3
    //}
}
