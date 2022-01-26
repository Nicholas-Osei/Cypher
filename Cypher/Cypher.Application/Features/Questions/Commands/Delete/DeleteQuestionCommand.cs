using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.Questions.Commands.Delete
{
    public class DeleteQuestionCommand: IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result<int>>
        {
            private readonly IQuestionRepository _questionRepo;
            private readonly IUnitOfWork _uow;

            public DeleteQuestionCommandHandler(IUnitOfWork uow, IQuestionRepository questionRepo)
            {
                _uow = uow;
                _questionRepo = questionRepo;
            }

            public async Task<Result<int>> Handle(DeleteQuestionCommand cmd, CancellationToken cancellationToken)
            {
                var question = await _questionRepo.GetByIdAsync(cmd.Id);
                await _questionRepo.DeleteAsync(question);
                await _uow.Commit(cancellationToken);
                return Result<int>.Success(question.Id);
            }
        }
    }
}
