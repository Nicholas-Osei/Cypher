﻿using AspNetCoreHero.Results;
using Cypher.Application.Extensions;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Players.Queries.GetAllPaged
{
    public class GetAllPlayersQuery : IRequest<PaginatedResult<GetAllPlayersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllPlayersQuery(int? pageNr, int? pageSize)
        {
            PageNumber = pageNr ?? 0;
            PageSize = pageSize ?? 10;
        }

        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, PaginatedResult<GetAllPlayersResponse>>
        {
            private readonly IPlayerRepository _repo;
            public GetAllPlayersQueryHandler(IPlayerRepository repository)
            {
                _repo = repository;
            }

            public async Task<PaginatedResult<GetAllPlayersResponse>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Player, GetAllPlayersResponse>> expression = e => new GetAllPlayersResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    IsAdmin = e.IsAdmin
                };
                var paginatedList = await _repo.Players
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
