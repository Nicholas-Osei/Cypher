using AutoMapper;
using Cypher.Application.Features.Players.Commands.Create;
using Cypher.Application.Features.Players.Queries.GetById;
using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Mappings
{
    internal class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<CreatePlayerCommand, Player>().ReverseMap();
            CreateMap<Player, GetPlayerByIdResponse>()
                .ForMember(dto => dto.Friends, opt => opt.MapFrom(x => x.Friends.Select(y => y.Friend)));
        }
    }
}
