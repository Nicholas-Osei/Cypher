using System;
using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;

namespace Cypher.Domain.Entities.Catalog
{
    public class Credentials: AuditableEntity
    {
        
        public string credentials { get; set; }
    }
}
