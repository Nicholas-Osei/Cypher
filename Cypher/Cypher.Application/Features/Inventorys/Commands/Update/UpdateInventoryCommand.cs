using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.Inventorys.Commands.Update
{
    public class UpdateInventoryCommand : IRequest<Result<int>>
    {
        public UpdateInventoryCommand()
        {
        }

        public int Id { get; set; }
        public virtual List<Item> Items { get; set; }

        public class UpdateIventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Result<int>>
        {
            private readonly IInventoryRepository _inventoryRepo;
            private readonly IUnitOfWork _uow;

            public UpdateIventoryCommandHandler(IUnitOfWork uow, IInventoryRepository inventoryRepo)
            {
                _uow = uow;
                _inventoryRepo = inventoryRepo;
            }

            public async Task<Result<int>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
            {
                var inventory = await _inventoryRepo.GetByIdAsync(request.Id);

                if (inventory == null)
                {
                    return Result<int>.Fail($"Item Not Found.");
                }
                else
                {
                    inventory.Items = request.Items ?? inventory.Items;
                    //item.Name = request.Name ?? item.Name;
                    //item.ItemType = request.ItemType ?? item.ItemType;
                    //item.Inventory = request.Inventory ?? item.Inventory;


                    await _inventoryRepo.UpdateAsync(inventory);
                    await _uow.Commit(cancellationToken);
                    return Result<int>.Success(inventory.Id);
                }
            }


        }
    }
}

