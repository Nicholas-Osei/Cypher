using Cypher.Application.Features.Brands.Commands.Create;
using Cypher.Application.Features.Brands.Commands.Update;
using Cypher.Application.Features.Brands.Queries.GetAllCached;
using Cypher.Application.Features.Brands.Queries.GetById;
using Cypher.Web.Areas.Catalog.Models;
using AutoMapper;

namespace Cypher.Web.Areas.Catalog.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<GetAllBrandsCachedResponse, BrandViewModel>().ReverseMap();
            CreateMap<GetBrandByIdResponse, BrandViewModel>().ReverseMap();
            CreateMap<CreateBrandCommand, BrandViewModel>().ReverseMap();
            CreateMap<UpdateBrandCommand, BrandViewModel>().ReverseMap();
        }
    }
}