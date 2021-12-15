using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.Messages.Commands.Create
{
    public class CreateMessageCommand: IRequest<Result<int>>
    {
        public string MessageText { get; set; }
        public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<int>>
        {
            private readonly IMessageRepository _messageRepo;
            private readonly IMapper _mapper;

            private IUnitOfWork _uow { get; set; }

            public CreateMessageCommandHandler(IMessageRepository messageRepo, IMapper mapper, IUnitOfWork uow)
            {
                _messageRepo = messageRepo;
                _mapper = mapper;
                _uow = uow;
            }

            public async Task<Result<int>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<Message>(request);
                await _messageRepo.InsertAsync(item);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }

           
        }
    }
}
