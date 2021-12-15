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

namespace Cypher.Application.Features.Lobbies.CMDs.Create
{
    public class CreateLobbyCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public Player LobbyAdmin { get; set; }
        public ICollection<Player> Players { get; set; }

        public class CreateLobbyCommandHandler : IRequestHandler<CreateLobbyCommand, Result<int>>
        {
            private readonly ILobbyRepository _repo;
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public CreateLobbyCommandHandler(ILobbyRepository repo, IUnitOfWork uow, IMapper mapper)
            {
                _repo = repo;
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
            {
                var lobby = _mapper.Map<Lobby>(request);
                await _repo.InsertAsync(lobby);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(lobby.Id);
            }
        }
    }
}
