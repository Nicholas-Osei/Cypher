using System;
using AspNetCoreHero.Abstractions.Domain;

namespace Cypher.Domain.Entities.Cypher
{
    public class UserCredentials: AuditableEntity
    {
        public new int Id { get; set; }
        public string Base64Credential { get; set; }
    }
}
