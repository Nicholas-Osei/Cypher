using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.Queries
{
    public class GetAllLobbiesQuery : IRequest<PaginatedResult<GetAllLobbiesQuery>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public GetAllLobbiesQuery(int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber ?? 0;
            PageSize = pageSize ?? 10;
        }

        public class GetAllLobbiesQueryHandler : IRequestHandler<GetAllLobbiesQuery, PaginatedResult<GetAllLobbiesResponse>>
        {
            private readonly ILobbyRepository _repo;

        }
    }
}
