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

namespace Cypher.Application.Features.Lobbies.CMDs.Update
{
    public class UpdateLobbyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LobbyAdminId { get; set; }
        public ICollection<int> PlayerIds { get; set; }

        public class UpdateLobbyCommandHandler : IRequestHandler<UpdateLobbyCommand, Result<int>>
        {
            private readonly ILobbyRepository _lobbyRepository;
            private readonly IPlayerRepository _playerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateLobbyCommandHandler(ILobbyRepository lobbyRepository, IUnitOfWork unitOfWork, IPlayerRepository playerRepository)
            {
                _lobbyRepository = lobbyRepository;
                _unitOfWork = unitOfWork;
                _playerRepository = playerRepository;
            }

            public async Task<Result<int>> Handle(UpdateLobbyCommand request, CancellationToken cancellationToken)
            {
                var lobby = await _lobbyRepository.GetByIdAsync(request.Id);

                if (lobby == null)
                {
                    return Result<int>.Fail($"Lobby Not Found.");
                }
                else
                {
                    lobby.Name = request.Name ?? lobby.Name;
                    lobby.LobbyAdminId = request.LobbyAdminId;

                    if (request.PlayerIds.Count != 0)
                    {
                        //if (lobby.Players == null)
                        //    lobby.Players = new List<Player>();
                        lobby.Players = new List<Player>();

                        foreach (var playerId in request.PlayerIds)
                        {
                            var player = await _playerRepository.GetByIdAsync(playerId);
                            lobby.Players.Add(player);
                        }
                    }
                    //lobby.Players = request.Players;

                    await _lobbyRepository.UpdateAsync(lobby);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(lobby.Id);
                }
            }
        }
    }
}
