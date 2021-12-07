﻿using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.Players.Commands.Delete
{
    public class DeleteFriendCommand : IRequest<Result<int>>
    {
        public int PlayerId { get; set; }
        public int FriendId { get; set; }

        public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, Result<int>>
        {
            private readonly IUnitOfWork _uow;
            private readonly IPlayerRepository _playerRepo;

            public DeleteFriendCommandHandler(IPlayerRepository playerRepo, IUnitOfWork uow)
            {
                _playerRepo = playerRepo;
                _uow = uow;
            }

            public async Task<Result<int>> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
            {
                var player = _playerRepo.GetByIdAsync(request.PlayerId);
                var friend = _playerRepo.GetByIdAsync(request.FriendId);

                //await _playerRepo.RemoveFriendAsync(player, friend);
                await _uow.Commit(cancellationToken);

                return Result<int>.Success(player.Id);
            }
        }
    }
}