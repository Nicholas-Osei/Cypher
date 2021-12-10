using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Inventorys.Commands.Create
{
    public partial class CreateInventoryCommand : IRequest<Result<int>>
    {
        public virtual ICollection<Item> Items { get; set; }
        public CreateInventoryCommand()
        {
        }
    }

        public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Result<int>>
        {
            private readonly IInventoryRepository _inventoryRepo;
            private readonly IMapper _mapper;

            private IUnitOfWork _uow { get; set; }

            public CreateInventoryCommandHandler(IInventoryRepository inventoryRepo, IMapper mapper, IUnitOfWork uow)
            {
                _inventoryRepo = inventoryRepo;
                _mapper = mapper;
                _uow = uow;
            }

            public async Task<Result<int>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
            {
                var inventory = _mapper.Map<Inventory>(request);
                await _inventoryRepo.InsertAsync(inventory);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(inventory.Id);
            }
        }
    
}
