using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cypher.Application.Features.UserCredentials.Commands.Create
{
    public partial class CreateUserCredentialsCommand: IRequest<Result<int>>
    {
        public string Base64Credential { get; set; }
  
    }
    public class CreatePlayerCommandHandler : IRequestHandler<CreateUserCredentialsCommand, Result<int>>
    {
        private readonly IUserCredentialsRepository usercredentialsRepo;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlayerCommandHandler(IUserCredentialsRepository usercredentialsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            usercredentialsRepo = usercredentialsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateUserCredentialsCommand request, CancellationToken cancellationToken)
        {
            var userCredentials = _mapper.Map<UserCredential>(request);
            await usercredentialsRepo.InsertAsync(userCredentials);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(userCredentials.Id);
        }
    }
}
