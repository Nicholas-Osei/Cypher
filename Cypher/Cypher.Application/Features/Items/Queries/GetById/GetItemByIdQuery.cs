using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Items.Queries.GetById
{
    public class GetItemByIdQuery : IRequest<Result<GetItemByIdResponse>>
    {
        public int Id { get; set; }

        public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Result<GetItemByIdResponse>>
        {
            private readonly IItemRepository _itemRepo;
            private readonly IMapper _mapper;

            public GetItemByIdQueryHandler(IMapper mapper, IItemRepository itemRepo)
            {
                _mapper = mapper;
                _itemRepo = itemRepo;
            }

            public async Task<Result<GetItemByIdResponse>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _itemRepo.GetByIdAsync(request.Id);
                var mappedItem = _mapper.Map<GetItemByIdResponse>(item);
                return Result<GetItemByIdResponse>.Success(mappedItem);
            }
        }
    }
}
