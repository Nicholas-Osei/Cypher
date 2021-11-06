using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using MediatR;

namespace Cypher.Application.Features.User_Credentials.Commands.Create
{
    public partial class CreateUserCredentialsComand : IRequest<Result<int>>
    {
        public string Base64Credential { get; set; }
      
    }

        public class CreateUserCredentialsCommandHandler : IRequestHandler<CreateUserCredentialsComand, Result<int>>
        {
            private readonly IUserCredentialsRepository _usercredentialsRepo;
            private readonly IMapper _mapper;

            private IUnitOfWork _unitOfWork { get; set; }

            public CreateUserCredentialsCommandHandler(IUserCredentialsRepository usercredentialsRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _usercredentialsRepo = usercredentialsRepository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(CreateUserCredentialsComand request, CancellationToken cancellationToken)
            {
                var player = _mapper.Map<UserCredentials>(request);
                await _usercredentialsRepo.InsertAsync(player);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(player.Id);
            }
        }
    
}
