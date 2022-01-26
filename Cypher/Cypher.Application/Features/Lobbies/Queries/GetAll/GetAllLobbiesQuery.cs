using AspNetCoreHero.Results;
using Cypher.Application.Extensions;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.Queries
{
    public class GetAllLobbiesQuery : IRequest<PaginatedResult<GetAllLobbiesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }

        public GetAllLobbiesQuery(int? pageNumber, int? pageSize, string userId)
        {
            PageNumber = pageNumber ?? 0;
            PageSize = pageSize ?? 100;
            UserId = userId;
        }

        public GetAllLobbiesQuery(int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber ?? 0;
            PageSize = pageSize ?? 100;
        }

        public class GetAllLobbiesQueryHandler : IRequestHandler<GetAllLobbiesQuery, PaginatedResult<GetAllLobbiesResponse>>
        {
            private readonly ILobbyRepository _repo;

            public GetAllLobbiesQueryHandler(ILobbyRepository repo)
            {
                _repo = repo;
            }

            public async Task<PaginatedResult<GetAllLobbiesResponse>> Handle(GetAllLobbiesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Lobby, GetAllLobbiesResponse>> expression = e => new GetAllLobbiesResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    LobbyAdmin = e.LobbyAdmin,
                    Players = e.Players,
                    CreatedOn = e.CreatedOn,
                    CreatedBy = e.CreatedBy
                };

                var lobbyList = _repo.Lobbies
                    .Include(l => l.LobbyAdmin)
                    .Include(l => l.Players)
                    .Select(expression);

                if (!string.IsNullOrWhiteSpace(request.UserId))
                    lobbyList = lobbyList.Where(l => l.CreatedBy == request.UserId);

                var paginatedList = await lobbyList
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;
            }
        }
    }
}
