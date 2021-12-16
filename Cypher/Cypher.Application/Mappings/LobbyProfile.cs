using AutoMapper;
using Cypher.Application.Features.Lobbies.CMDs.Create;
using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Mappings
{
    internal class LobbyProfile : Profile
    {
        public LobbyProfile()
        {
            CreateMap<CreateLobbyCommand, Lobby>().ReverseMap();
        }
    }
}
