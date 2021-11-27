using System;
using AutoMapper;
using Cypher.Application.Features.Inventorys.Commands.Create;
using Cypher.Application.Features.Inventorys.Queries.GetById;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Mappings
{
    internal class InventoryProfile: Profile
    {
        public InventoryProfile()
        {
            CreateMap<CreateInventoryCommand, Inventory>().ReverseMap();
            CreateMap<GetInventoryByIdResponse, Inventory>().ReverseMap();
        }
    }
}
