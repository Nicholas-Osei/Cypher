using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Messages.Commands.Delete
{
    public class DeleteMessageCommand: IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Result<int>>
        {
            private readonly IMessageRepository _messageRepo;
            private readonly IUnitOfWork _uow;

            public DeleteMessageCommandHandler(IUnitOfWork uow, IMessageRepository messageRepo)
            {
                _uow = uow;
                _messageRepo = messageRepo;
            }

            public async Task<Result<int>> Handle(DeleteMessageCommand cmd, CancellationToken cancellationToken)
            {
                var message = await _messageRepo.GetByIdAsync(cmd.Id);
                await _messageRepo.DeleteAsync(message);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(message.Id);
            }
        }
    }
}
