using Cypher.Application.Features.Brands.Commands.Create;
using Cypher.Application.Features.Brands.Queries.GetAllCached;
using Cypher.Application.Features.Brands.Queries.GetById;
using Cypher.Domain.Entities.Catalog;
using AutoMapper;

namespace Cypher.Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}