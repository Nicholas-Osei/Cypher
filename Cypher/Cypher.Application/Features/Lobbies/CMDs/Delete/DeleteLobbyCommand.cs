using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.CMDs.Delete
{
    public class DeleteLobbyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteLobbyCommandHandler : IRequestHandler<DeleteLobbyCommand, Result<int>>
        {
            private readonly ILobbyRepository _lobbyRepo;
            private readonly IUnitOfWork _uow;

            public DeleteLobbyCommandHandler(ILobbyRepository lobbyRepo, IUnitOfWork uow)
            {
                _lobbyRepo = lobbyRepo;
                _uow = uow;
            }

            public async Task<Result<int>> Handle(DeleteLobbyCommand request, CancellationToken cancellationToken)
            {
                var lobby = await _lobbyRepo.GetByIdAsync(request.Id);
                await _lobbyRepo.DeleteAsync(lobby);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(lobby.Id);
            }
        }
    }
}
