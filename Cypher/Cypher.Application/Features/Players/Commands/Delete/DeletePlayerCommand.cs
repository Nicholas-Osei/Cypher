using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Players.Commands.Delete
{
    public class DeletePlayerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, Result<int>>
        {
            private readonly IPlayerRepository _playerRepo;
            private readonly IUnitOfWork _uow;

            public DeletePlayerCommandHandler(IUnitOfWork uow, IPlayerRepository playerRepo)
            {
                _uow = uow;
                _playerRepo = playerRepo;
            }

            public async Task<Result<int>> Handle(DeletePlayerCommand cmd, CancellationToken cancellationToken)
            {
                var player = await _playerRepo.GetByIdAsync(cmd.Id);
                await _playerRepo.DeleteAsync(player);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(player.Id);
            }
        }
    }
}