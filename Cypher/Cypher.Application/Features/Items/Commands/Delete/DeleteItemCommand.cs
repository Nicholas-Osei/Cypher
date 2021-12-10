using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Items.Commands.Delete
{
    public class DeleteItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result<int>>
        {
            private readonly IItemRepository _itemRepo;
            private readonly IUnitOfWork _uow;

            public DeleteItemCommandHandler(IUnitOfWork uow, IItemRepository itemRepo)
            {
                _uow = uow;
                _itemRepo = itemRepo;
            }

            public async Task<Result<int>> Handle(DeleteItemCommand cmd, CancellationToken cancellationToken)
            {
                var item = await _itemRepo.GetByIdAsync(cmd.Id);
                await _itemRepo.DeleteAsync(item);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
