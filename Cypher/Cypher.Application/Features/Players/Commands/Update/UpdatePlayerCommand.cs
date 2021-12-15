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

namespace Cypher.Application.Features.Players.Commands.Update
{
    public class UpdatePlayerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        // TO DO: Update many to many relations or create seperate commands

        public virtual Inventory Inventory { get; set; }

        public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPlayerRepository _playerRepo;

            public UpdatePlayerCommandHandler(IPlayerRepository playerRepo, IUnitOfWork unitOfWork)
            {
                _playerRepo = playerRepo;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdatePlayerCommand cmd, CancellationToken cancellationToken)
            {
                var player = await _playerRepo.GetByIdAsync(cmd.Id);

                if (player == null)
                {
                    return Result<int>.Fail($"Player Not Found.");
                }
                else
                {
                    player.Name = cmd.Name ?? player.Name;
                    // For lobby admin: Needs to be added to list
                    //player.IsAdmin = cmd.IsAdmin;
                    player.Inventory = cmd.Inventory ?? player.Inventory;

                    await _playerRepo.UpdateAsync(player);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(player.Id);
                }
            }
        }
    }
}
