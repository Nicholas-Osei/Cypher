using Cypher.Application.Features.Products.Commands.Create;
using Cypher.Application.Features.Products.Commands.Update;
using Cypher.Application.Features.Products.Queries.GetAllCached;
using Cypher.Application.Features.Products.Queries.GetById;
using Cypher.Web.Areas.Catalog.Models;
using AutoMapper;

namespace Cypher.Web.Areas.Catalog.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsCachedResponse, ProductViewModel>().ReverseMap();
            CreateMap<GetProductByIdResponse, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductViewModel>().ReverseMap();
        }
    }
}