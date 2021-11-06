using System;
using AutoMapper;
using Cypher.Application.Features.User_Credentials.Commands.Create;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Mappings
{
    internal class UserCredentialsProfile:Profile
    {
        public UserCredentialsProfile()
        {
            CreateMap<CreateUserCredentialsComand, UserCredentials>().ReverseMap();
        }
    }
}
