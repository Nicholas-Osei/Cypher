using System;
using AspNetCoreHero.Abstractions.Domain;

namespace Cypher.Domain.Entities.Cypher
{
    public class Question: AuditableEntity
    {
       
        public string question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
    }
}
