using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Items.Commands.Update
{
    public class UpdateItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }

        // Needs to be able to be put in an Inventory

        public virtual Inventory Inventory { get; set; }

        public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result<int>>
        {
            private readonly IItemRepository _itemRepo;
            private readonly IUnitOfWork _uow;

            public UpdateItemCommandHandler(IUnitOfWork uow, IItemRepository itemRepo)
            {
                _uow = uow;
                _itemRepo = itemRepo;
            }

            public async Task<Result<int>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
            {
                var item = await _itemRepo.GetByIdAsync(request.Id);

                if (item == null)
                {
                    return Result<int>.Fail($"Item Not Found.");
                }
                else
                {
                    
                    item.Name = request.Name ?? item.Name;
                    item.ItemType = request.ItemType ?? item.ItemType;
                    item.Inventory = request.Inventory ?? item.Inventory;

                    await _itemRepo.UpdateAsync(item);
                    await _uow.Commit(cancellationToken);
                    return Result<int>.Success(item.Id);
                }
            }
        }
    }
}
