using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Items.Commands.Create
{
    public class CreateItemCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string ItemType { get; set; }
        
        // Does not have to be linked to an inventory on creation

        public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<int>>
        {
            private readonly IItemRepository _itemRepo;
            private readonly IMapper _mapper;

            private IUnitOfWork _uow { get; set; }

            public CreateItemCommandHandler(IItemRepository itemRepo, IMapper mapper, IUnitOfWork uow)
            {
                _itemRepo = itemRepo;
                _mapper = mapper;
                _uow = uow;
            }

            public async Task<Result<int>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<Item>(request);
                await _itemRepo.InsertAsync(item);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
