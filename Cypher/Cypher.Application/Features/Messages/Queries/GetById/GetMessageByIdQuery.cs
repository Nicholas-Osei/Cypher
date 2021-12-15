using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Messages.Queries.GetById
{
    public class GetMessageByIdQuery: IRequest<Result<GetMessageByIdResponse>>
    {
        public int Id { get; set; }
        public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, Result<GetMessageByIdResponse>>
        {
            private readonly IMessageRepository _messageRepo;
            private readonly IMapper _mapper;

            public GetMessageByIdQueryHandler(IMapper mapper, IMessageRepository messageRepo)
            {
                _mapper = mapper;
                _messageRepo = messageRepo;
            }

            public async Task<Result<GetMessageByIdResponse>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
            {
                var message = await _messageRepo.GetByIdAsync(request.Id);
                var mappedItem = _mapper.Map<GetMessageByIdResponse>(message);
                return Result<GetMessageByIdResponse>.Success(mappedItem);
            }
        }

    }
}
