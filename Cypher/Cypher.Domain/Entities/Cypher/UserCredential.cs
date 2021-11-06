using System;
using AspNetCoreHero.Abstractions.Domain;

namespace Cypher.Domain.Entities.Cypher
{
    public class UserCredential: AuditableEntity
    {
        public string Base64Credential { get; set; }
        public UserCredential()
        {
        }
    }
}
