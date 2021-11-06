using System;
namespace Cypher.Application.Features.User_Credentials.Queries.GetAllUserCredentials
{
    public class GetAllUserCredentialsResponse
    {
        public int Id { get; set; }
        public string Base64Credential { get; set; }
        public GetAllUserCredentialsResponse()
        {
        }
    }
}
