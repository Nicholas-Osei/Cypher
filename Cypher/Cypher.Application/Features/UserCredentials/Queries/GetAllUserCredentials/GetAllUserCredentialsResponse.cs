using System;
namespace Cypher.Application.Features.UserCredentials.Queries.GetAllUserCredentials
{
    public class GetAllUserCredentialsResponse
    {
        public GetAllUserCredentialsResponse()
        {
        }
        public int Id { get; set; }
        public string Base64Credential { get; set; }
    }
}
