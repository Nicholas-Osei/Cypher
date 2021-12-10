using System;
using AutoMapper;
using Cypher.Application.Features.UserCredentials.Commands.Create;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Mappings
{
    public class UserCredentialProfile:Profile
    {
        public UserCredentialProfile()
        {
            CreateMap<CreateUserCredentialsCommand, UserCredential>().ReverseMap();
        }
    }
}
