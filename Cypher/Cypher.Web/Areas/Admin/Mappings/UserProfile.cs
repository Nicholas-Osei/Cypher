using Cypher.Infrastructure.Identity.Models;
using Cypher.Web.Areas.Admin.Models;
using AutoMapper;

namespace Cypher.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}