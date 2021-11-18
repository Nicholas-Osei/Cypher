using AutoMapper;
using Cypher.Application.Features.Items.Commands.Create;
using Cypher.Domain.Entities.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Mappings
{
    internal class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<CreateItemCommand, Item>().ReverseMap();
        }
    }
}
