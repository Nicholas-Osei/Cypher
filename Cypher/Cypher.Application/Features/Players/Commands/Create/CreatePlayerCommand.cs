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

namespace Cypher.Application.Features.Players.Commands.Create
{
    public partial class CreatePlayerCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        // Rest of properties should be initiated when needed?
        // A player should be linked to a account
        public virtual Inventory Inventory { get; set; }
    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Result<int>>
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlayerCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _playerRepo = playerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = _mapper.Map<Player>(request);
            await _playerRepo.InsertAsync(player);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(player.Id);
        }
    }
}
