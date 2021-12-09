using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.Messages.Commands.Update
{
    public class UpdateMessageCommand: IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string MessageText { get; set; }

        public virtual Player Player { get; set; }

        public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Result<int>>
        {
            private readonly IMessageRepository _messageRepo;
            private readonly IUnitOfWork _uow;

            public UpdateMessageCommandHandler(IUnitOfWork uow, IMessageRepository messageRepo)
            {
                _uow = uow;
                _messageRepo = messageRepo;
            }

            public async Task<Result<int>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
            {
                var message = await _messageRepo.GetByIdAsync(request.Id);

                if (message == null)
                {
                    return Result<int>.Fail($"No message Found.");
                }
                else
                {

                    message.MessageText = request.MessageText ?? message.MessageText;
                    //item.Inventory = request.Inventory ?? item.Inventory;

                    await _messageRepo.UpdateAsync(message);
                    await _uow.Commit(cancellationToken);
                    return Result<int>.Success(message.Id);
                }
            }
        }

    }
}
