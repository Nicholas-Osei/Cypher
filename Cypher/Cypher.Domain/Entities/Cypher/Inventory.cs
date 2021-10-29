﻿using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Domain.Entities.Cypher
{
    public class Inventory : BaseEntity
    {
        public Player Owner { get; set; }
        public virtual ICollection<Item> Items { get; set; }

    }
}
