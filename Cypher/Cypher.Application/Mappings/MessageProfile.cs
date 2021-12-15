using System;
using AutoMapper;
using Cypher.Application.Features.Messages.Commands.Create;
using Cypher.Application.Features.Messages.Queries.GetById;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Mappings
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<CreateMessageCommand, Message>().ReverseMap();
            CreateMap<GetMessageByIdResponse, Message>().ReverseMap();
        }
    }
}
