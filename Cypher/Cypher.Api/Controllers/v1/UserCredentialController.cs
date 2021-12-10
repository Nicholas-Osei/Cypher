using System;
using System.Threading.Tasks;
using Cypher.API.Controllers;
using Cypher.Application.Features.UserCredentials.Commands.Create;
using Cypher.Application.Features.UserCredentials.Queries.GetAllUserCredentials;
using Microsoft.AspNetCore.Mvc;

namespace Cypher.Api.Controllers.v1
{
    public class UserCredentialController: BaseApiController<UserCredentialController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var usercredentials = await _mediator.Send(new GetAllUserCredentialsQuery(pageNumber, pageSize));
            return Ok(usercredentials);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCredentialsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
