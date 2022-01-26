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

namespace Cypher.Application.Features.Questions.Queries.GetAllQuestions
{
   
    public class GetAllQuestionsQuery : IRequest<PaginatedResult<GetAllQuestionsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllQuestionsQuery(int pageNr, int pageSize)
        {
            PageNumber = pageNr;
            PageSize = pageSize;
        }

        public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, PaginatedResult<GetAllQuestionsResponse>>
        {
            private readonly IQuestionRepository _repo;
            public GetAllQuestionsQueryHandler(IQuestionRepository repository)
            {
                _repo = repository;
            }

            public async Task<PaginatedResult<GetAllQuestionsResponse>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Question, GetAllQuestionsResponse>> expression = e => new GetAllQuestionsResponse
                {
                    Id = e.Id,
                    question=e.question,
                    Answer=e.Answer,
                    Location=e.Location,
                    Option1=e.Option1,
                    Option2=e.Option2,
                    Type=e.Type

                    

                };
                var paginatedList = await _repo.questions
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
        }
    }
}
