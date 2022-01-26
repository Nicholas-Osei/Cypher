using AspNetCoreHero.Results;
using Cypher.Application.Extensions;
using Cypher.Application.Features.Items.Queries;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Cypher.Application.Features.Items.Queries.GetAllItemsQuery;


namespace Cypher.Application.Features.Players.Queries.GetAllPaged
{
    public class GetAllPlayersQuery : IRequest<PaginatedResult<GetAllPlayersResponse>>
    {
        public string NameQuery { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }

        public GetAllPlayersQuery(string playerName, int? pageNr, int? pageSize, string userId)
        {
            NameQuery = playerName;
            PageNumber = pageNr ?? 0;
            PageSize = pageSize ?? 10;
            UserId = userId;
        }

        public GetAllPlayersQuery(string playerName, int? pageNr, int? pageSize)
        {
            NameQuery = playerName;
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
                    //inventory = e.Inventory,
                    CreatedOn = e.CreatedOn,
                    CreatedBy = e.CreatedBy
                    //IsAdmin = e.IsAdmin,
                    //check = e.Inventory.Items.Count,
                    //Items = e.Inventory.Items,
                    //Messages = e.MessagePlayers,
                    //Lobbies = e.LobbiesJoined
                    
                };

                //var playerList = _repo.Players.Include(m=>m.MessagePlayers).Include(p => p.Inventory)
                //    .ThenInclude(i => i.Items)
                //    .Select(expression);
                var playerList = _repo.Players
                    .Select(expression);

                if (!string.IsNullOrWhiteSpace(request.NameQuery))
                    playerList = playerList.Where(p => p.Name == request.NameQuery);

                if (!string.IsNullOrWhiteSpace(request.UserId))
                    playerList = playerList.Where(p => p.CreatedBy == request.UserId);

                var paginatedList = await playerList
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;
            }
        }
    }
}
