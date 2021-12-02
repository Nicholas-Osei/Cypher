using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.CacheRepositories;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Players.Queries.GetById
{
    public class GetPlayerByIdQuery : IRequest<Result<GetPlayerByIdResponse>>
    {
        public int Id { get; set; }

        public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Result<GetPlayerByIdResponse>>
        {
            private readonly IPlayerRepository _playerRepo;
            private readonly IMapper _mapper;

            public GetPlayerByIdQueryHandler(IMapper mapper, IPlayerRepository playerRepo)
            {
                _mapper = mapper;
                _playerRepo = playerRepo;
            }

            public async Task<Result<GetPlayerByIdResponse>> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
            {
                var player = await _playerRepo.GetByIdAsync(query.Id);
                var mappedPlayer = _mapper.Map<GetPlayerByIdResponse>(player);
                return Result<GetPlayerByIdResponse>.Success(mappedPlayer);
            }
        }
    }
}