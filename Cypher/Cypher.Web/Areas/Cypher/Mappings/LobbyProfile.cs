using AutoMapper;
using Cypher.Application.Features.Lobbies.CMDs.Create;
using Cypher.Application.Features.Lobbies.Queries;
using Cypher.Web.Areas.Cypher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Web.Areas.Cypher.Mappings
{
    internal class LobbyProfile : Profile
    {
        public LobbyProfile()
        {
            CreateMap<CreateLobbyCommand, LobbyViewModel>().ReverseMap();
            CreateMap<GetAllLobbiesResponse, LobbyViewModel>().ReverseMap();
        }
    }
}
