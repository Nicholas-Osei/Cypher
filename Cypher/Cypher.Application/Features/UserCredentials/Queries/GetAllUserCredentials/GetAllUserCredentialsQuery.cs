using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Extensions;
using Cypher.Application.Features.User_Credentials.Queries.GetAllUserCredentials;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.User_Credentials.Queries.GetAllCredentials
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

        public class GetAllUserCredentialsQueryHandler : IRequestHandler<GetAllUserCredentialsQuery, PaginatedResult<GetAllUserCredentialsResponse>>
        {
            private readonly IUserCredentialsRepository _repo;
            public GetAllUserCredentialsQueryHandler(IUserCredentialsRepository repository)
            {
                _repo = repository;
            }

            public async Task<PaginatedResult<GetAllUserCredentialsResponse>> Handle(GetAllUserCredentialsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<UserCredentials, GetAllUserCredentialsResponse>> expression = c => new GetAllUserCredentialsResponse
                {
                    Id = c.Id,
                    Base64Credential = c.Base64Credential
                };
                var paginatedList = await _repo.UserCredentials
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
