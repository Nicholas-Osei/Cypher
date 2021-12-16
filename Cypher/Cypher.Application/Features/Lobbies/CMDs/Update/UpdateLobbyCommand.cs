using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.CMDs.Update
{
    public class UpdateLobbyCommand : IRequest<Result<int>>
    {
    }
}
