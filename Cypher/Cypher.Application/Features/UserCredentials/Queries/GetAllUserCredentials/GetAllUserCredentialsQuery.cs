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

namespace Cypher.Application.Features.UserCredentials.Queries.GetAllUserCredentials
{
    public class GetAllUserCredentialsQuery: IRequest<PaginatedResult<GetAllUserCredentialsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllUserCredentialsQuery(int pageNr, int pageSize)
        {
            PageNumber = pageNr;
            PageSize = pageSize;
        }

        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllUserCredentialsQuery, PaginatedResult<GetAllUserCredentialsResponse>>
        {
            private readonly IUserCredentialsRepository _repo;
            public GetAllPlayersQueryHandler(IUserCredentialsRepository repository)
            {
                _repo = repository;
            }

            public async Task<PaginatedResult<GetAllUserCredentialsResponse>> Handle(GetAllUserCredentialsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<UserCredential, GetAllUserCredentialsResponse>> expression = e => new GetAllUserCredentialsResponse
                {
                    Id = e.Id,
                    Base64Credential = e.Base64Credential
                   
                };
                var paginatedList = await _repo.UserCredential
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
