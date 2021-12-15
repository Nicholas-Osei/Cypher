using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Extensions;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.Messages.Queries.GetAll
{
    public class GetAllMessagesQuery: IRequest<PaginatedResult<GetAllMessageResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllMessagesQuery(int? pageNumber, int? pageSize)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 0;
        }
        public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, PaginatedResult<GetAllMessageResponse>>
        {
            private readonly IMessageRepository _repo;
            public GetAllMessagesQueryHandler(IMessageRepository repo)
            {
                _repo = repo;
            }

            public async Task<PaginatedResult<GetAllMessageResponse>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Message, GetAllMessageResponse>> expression = e => new GetAllMessageResponse
                {
                  Id = e.Id,
                  Sender = e.Sender,
                  MessageText = e.MessageText
                };
                var paginatedList = await _repo.Messages
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
