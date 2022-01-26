﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Features.Players.Queries.GetAllPaged;
using Cypher.Domain.Entities.Cypher;
using AutoMapper;
using Cypher.Web.Areas.Cypher.Models;
using Cypher.Application.Features.Players.Queries.GetById;

namespace Cypher.Web.Areas.Cypher.Mappings
{
    internal class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<GetAllPlayersResponse, Player>().ReverseMap();
            CreateMap<GetAllPlayersResponse, PlayerViewModel>().ReverseMap();
            CreateMap<GetPlayerByIdResponse, PlayerViewModel>().ReverseMap();
        }
    }
}
