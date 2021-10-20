using Cypher.Application.Features.Products.Commands.Create;
using Cypher.Application.Features.Products.Queries.GetAllCached;
using Cypher.Application.Features.Products.Queries.GetAllPaged;
using Cypher.Application.Features.Products.Queries.GetById;
using Cypher.Domain.Entities.Catalog;
using AutoMapper;

namespace Cypher.Application.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsCachedResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        }
    }
}