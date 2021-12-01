using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Extensions;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.Inventorys.Queries.GetAll
{
    public class GetAllInventoryQuery: IRequest<PaginatedResult<GetAllInventoryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllInventoryQuery(int? pageNumber, int? pageSize)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 0;
        }

        public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, PaginatedResult<GetAllInventoryResponse>>
        {
            private readonly IInventoryRepository _inventoryRepo;

            public GetAllInventoryQueryHandler(IInventoryRepository repo)
            {
                _inventoryRepo = repo;
            }

            public async Task<PaginatedResult<GetAllInventoryResponse>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Inventory, GetAllInventoryResponse>> expression = e => new GetAllInventoryResponse
                {
                    Id = e.Id,
                    Items = e.Items
                };
                var paginatedList = await _inventoryRepo.Inventories
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
