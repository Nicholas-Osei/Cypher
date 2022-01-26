using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Questions.Commands.Update
{
  
    public class UpdateQuestionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }


        public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IQuestionRepository _questionRepo;

            public UpdateQuestionCommandHandler(IQuestionRepository questionRepo, IUnitOfWork unitOfWork)
            {
                _questionRepo = questionRepo;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateQuestionCommand cmd, CancellationToken cancellationToken)
            {
                var _question = await _questionRepo.GetByIdAsync(cmd.Id);

                if (_question == null)
                {
                    return Result<int>.Fail($"Question Not Found.");
                }
                else
                {
                    _question.question = cmd.question ?? _question.question;
                    _question.Answer = cmd.Answer ?? _question.Answer;
                    _question.Type = cmd.Type ?? _question.Type;
                    _question.Location = cmd.Location ?? _question.Location;
                    _question.Option1 = cmd.Option1 ?? _question.Option1;
                    _question.Option2 = cmd.Option2 ?? _question.Option2;

                    await _questionRepo.UpdateAsync(_question);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(_question.Id);
                }
            }
        }
    }
}
