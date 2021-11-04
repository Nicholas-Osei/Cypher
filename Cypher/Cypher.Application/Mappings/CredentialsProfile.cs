using System;
using AutoMapper;
using Cypher.Application.Features.Credentials.Queries.GetallCredentials;
using Cypher.Domain.Entities.Catalog;

namespace Cypher.Application.Mappings
{
    public class CredentialsProfile:Profile
    {
        public CredentialsProfile()
        {
            CreateMap<GetAllCredentialsVM, Credentials>().ReverseMap();
        }
    }
}
