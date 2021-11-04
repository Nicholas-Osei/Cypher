using System;
using System.Threading.Tasks;
using Cypher.API.Controllers;
using Cypher.Application.Features.Brands.Commands.Create;
using Cypher.Application.Features.Credentials.Queries.GetallCredentials;
using Microsoft.AspNetCore.Mvc;

namespace Cypher.Api.Controllers
{
    public class UserCredentialsController:BaseApiController<UserCredentialsController>
    {
        public UserCredentialsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creds = await _mediator.Send(new GetAllCredentialsQuery());
            return Ok(creds);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
