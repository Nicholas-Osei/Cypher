using AspNetCoreHero.Results;
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

namespace Cypher.Application.Features.Items.Queries
{
    public class GetAllItemsQuery : IRequest<PaginatedResult<GetAllItemsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllItemsQuery(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, PaginatedResult<GetAllItemsResponse>>
        {
            private readonly IItemRepository _repo;

            public GetAllItemsQueryHandler(IItemRepository repo)
            {
                _repo = repo;
            }

            public async Task<PaginatedResult<GetAllItemsResponse>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Item, GetAllItemsResponse>> expression = e => new GetAllItemsResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    ItemType = e.ItemType
                };
                var paginatedList = await _repo.Items
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
