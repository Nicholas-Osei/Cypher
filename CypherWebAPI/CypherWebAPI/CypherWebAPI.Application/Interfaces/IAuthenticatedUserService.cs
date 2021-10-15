using System;
using System.Collections.Generic;
using System.Text;

namespace CypherWebAPI.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
