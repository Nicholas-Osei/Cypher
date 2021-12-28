using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Lobbies.Queries.GetById
{
    public class GetLobbyByIdQuery : IRequest<Result<GetLobbyByIdResponse>>
    {
        public int Id { get; set; }

        public class GetLobbyByIdQueryHandler : IRequestHandler<GetLobbyByIdQuery, Result<GetLobbyByIdResponse>>
        {
            private readonly ILobbyRepository _lobbyRepo;
            private readonly IMapper _mapper;

            public GetLobbyByIdQueryHandler(ILobbyRepository lobbyRepo, IMapper mapper)
            {
                _lobbyRepo = lobbyRepo;
                _mapper = mapper;
            }

            public async Task<Result<GetLobbyByIdResponse>> Handle(GetLobbyByIdQuery request, CancellationToken cancellationToken)
            {
                var lobby = await _lobbyRepo.GetByIdAsync(request.Id);
                var mappedLobby = _mapper.Map<GetLobbyByIdResponse>(lobby);
                return Result<GetLobbyByIdResponse>.Success(mappedLobby);
            }
        }
    }
}
