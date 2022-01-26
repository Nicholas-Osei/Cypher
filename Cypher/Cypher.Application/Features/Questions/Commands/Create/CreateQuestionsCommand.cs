using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.Questions.Commands.Create
{
   
    public partial class CreateQuestionsCommand : IRequest<Result<int>>
    {
        public string question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }

    }
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionsCommand, Result<int>>
    {
        private readonly IQuestionRepository questionsRepo;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateQuestionCommandHandler(IQuestionRepository questionsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            questionsRepo = questionsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateQuestionsCommand request, CancellationToken cancellationToken)
        {
            var questions = _mapper.Map<Question>(request);
            await questionsRepo.InsertAsync(questions);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(questions.Id);
        }
    }
}
