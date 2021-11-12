using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.CacheRepositories;
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
            private readonly IPlayerCacheRepository _playerCacheRepo;
            private readonly IMapper _mapper;

            public GetPlayerByIdQueryHandler(IMapper mapper, IPlayerCacheRepository playerCacheRepo)
            {
                _mapper = mapper;
                _playerCacheRepo = playerCacheRepo;
            }

            public async Task<Result<GetPlayerByIdResponse>> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
            {
                var player = await _playerCacheRepo.GetByIdAsync(query.Id);
                var mappedPlayer = _mapper.Map<GetPlayerByIdResponse>(player);
                return Result<GetPlayerByIdResponse>.Success(mappedPlayer);
            }
        }
    }
}