using AutoMapper;
using CypherWebAPI.Application.Features.Products.Commands.CreateProduct;
using CypherWebAPI.Application.Features.Products.Queries.GetAllProducts;
using CypherWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CypherWebAPI.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
