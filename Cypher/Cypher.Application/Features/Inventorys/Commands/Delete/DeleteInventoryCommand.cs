using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Inventorys.Commands.Delete
{
    public class DeleteInventoryCommand: IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteInventoryCommand()
        {
        }

        public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Result<int>>
        {
            private readonly IInventoryRepository _inventoryRepo;
            private readonly IUnitOfWork _uow;

            public DeleteInventoryCommandHandler(IUnitOfWork uow, IInventoryRepository inventoryRepo)
            {
                _uow = uow;
                _inventoryRepo = inventoryRepo;
            }

            public async Task<Result<int>> Handle(DeleteInventoryCommand cmd, CancellationToken cancellationToken)
            {
                var item = await _inventoryRepo.GetByIdAsync(cmd.Id);
                await _inventoryRepo.DeleteAsync(item);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
