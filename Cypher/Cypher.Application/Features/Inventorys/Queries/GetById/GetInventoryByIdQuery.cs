using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Inventorys.Queries.GetById
{
    public class GetInventoryByIdQuery:IRequest<Result<GetInventoryByIdResponse>>
    {

        public int Id { get; set; }

        public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, Result<GetInventoryByIdResponse>>
        {
            private readonly IInventoryRepository _inventoryRepo;
            private readonly IMapper _mapper;

            public GetInventoryByIdQueryHandler(IMapper mapper, IInventoryRepository inventoryRepo)
            {
                _mapper = mapper;
                _inventoryRepo = inventoryRepo;
            }

            public async Task<Result<GetInventoryByIdResponse>> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
            {
                var inventory = await _inventoryRepo.GetByIdAsync(request.Id);
                var mappedItem = _mapper.Map<GetInventoryByIdResponse>(inventory);
                return Result<GetInventoryByIdResponse>.Success(mappedItem);
            }
        }
    }
}
