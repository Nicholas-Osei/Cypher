using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Questions.Queries.GetById
{
    public class GetQuestionByIdQuery: IRequest<Result<GetQuestionByIdResponse>>
    {
        public int Id { get; set; }
        public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Result<GetQuestionByIdResponse>>
        {
            private readonly IQuestionRepository _questionRepo;
            private readonly IMapper _mapper;

            public GetQuestionByIdQueryHandler(IMapper mapper, IQuestionRepository playerRepo)
            {
                _mapper = mapper;
                _questionRepo = playerRepo;
            }

            public async Task<Result<GetQuestionByIdResponse>> Handle(GetQuestionByIdQuery query, CancellationToken cancellationToken)
            {
                var question = await _questionRepo.GetByIdAsync(query.Id);
                var mappedQuestion = _mapper.Map<GetQuestionByIdResponse>(question);
                return Result<GetQuestionByIdResponse>.Success(mappedQuestion);
            }
        }
    }
}
