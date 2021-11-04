using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using Cypher.Application.Interfaces.Repositories;
using MediatR;

namespace Cypher.Application.Features.CredentialsMap.Commands.Create
{
    public partial class CreateCredentialsCommand:IRequest<CreateCredentialsCommand>
    {
        public string Name { get; set; }
    }

    //public class AddCredentialsHandler : IRequestHandler<Credentials, Result<int>>
    //{
    //    private readonly ICredentials repo;
    //    private readonly IMapper mapper;

    //    public AddCredentialsHandler(ICredentials repo, IMapper mapper)
    //    {
    //        this.repo = repo;
    //        this.mapper = mapper;
    //    }

    //    public async Task<Result<int>> Handle(CreateCredentialsCommand request, CancellationToken cancellationToken)
    //    {
    //        var credential = mapper.Map<Credentials>(request);
    //        await repo.InsertAsync(credential);
    //        return Result<int>.Success(credential.Id);
    //    }

        
    //}
}
