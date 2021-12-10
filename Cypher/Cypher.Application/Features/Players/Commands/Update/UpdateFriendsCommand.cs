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
    public class UpdateFriendsCommand : IRequest<Result<int>>
    {
        public int PlayerId { get; set; }
        public int FriendId { get; set; }

        public class UpdateFriendsCommandHandler : IRequestHandler<UpdateFriendsCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPlayerRepository _playerRepo;

            public UpdateFriendsCommandHandler(IPlayerRepository playerRepo, IUnitOfWork unitOfWork)
            {
                _playerRepo = playerRepo;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateFriendsCommand request, CancellationToken cancellationToken)
            {
                var player = await _playerRepo.GetByIdAsync(request.PlayerId);
                var friend = await _playerRepo.GetByIdAsync(request.FriendId);

                if (player == null)
                    return Result<int>.Fail($"Player Not Found.");
                else if (friend == null)
                    return Result<int>.Fail($"Player You Are Trying To Add Not Found.");
                else
                {
                    //if (player.Friends == null)
                    //    player.Friends = new List<PlayerFriend>();

                    //var pf = new PlayerFriend()
                    //{
                    //    PlayerId = player.Id,
                    //    Player = player,
                    //    FriendId = friend.Id,
                    //    Friend = friend
                    //};
                    //player.Friends.Add(pf);
                    if (player.Friends == null)
                        player.Friends = new List<Player>();

                    player.Friends.Add(friend);

                    await _playerRepo.UpdateAsync(player);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(player.Id);
                }
            }
        }
    }
}
