using AspNetCoreHero.Results;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.CMDs.Create
{
    public class CreateLobbyCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public Player LobbyAdmin { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
