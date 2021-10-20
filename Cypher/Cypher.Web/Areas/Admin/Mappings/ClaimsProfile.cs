using Cypher.Web.Areas.Admin.Models;
using AutoMapper;
using System.Security.Claims;

namespace Cypher.Web.Areas.Admin.Mappings
{
    public class ClaimsProfile : Profile
    {
        public ClaimsProfile()
        {
            CreateMap<Claim, RoleClaimsViewModel>().ReverseMap();
        }
    }
}