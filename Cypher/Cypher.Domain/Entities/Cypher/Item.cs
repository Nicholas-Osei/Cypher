﻿using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Item : AuditableEntity
    {
        public string Name { get; set; }
        public ItemType ItemType { get; set; }

        public virtual Inventory Inventory { get; set; }       
    }

    public enum ItemType
    {
        type1,
        type2,
        type3
    }
}