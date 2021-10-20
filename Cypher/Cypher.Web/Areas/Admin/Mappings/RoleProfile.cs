using Cypher.Web.Areas.Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Cypher.Web.Areas.Admin.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}