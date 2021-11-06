using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Cypher.API.Controllers;
using Cypher.Application.Features.User_Credentials.Commands.Create;
using Cypher.Application.Features.User_Credentials.Queries.GetAllCredentials;
using Cypher.Application.Interfaces.Contexts;
using Cypher.Domain.Entities.Catalog;
using Cypher.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Cypher.Api.Controllers.v1
{
   // [Route("api/v1/Login")]
    public class UserCredentialsController: BaseApiController<UserCredentialsController>
    {
        public UserCredentialsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var usercredentials = await _mediator.Send(new GetAllUserCredentialsQuery(pageNumber, pageSize));
            return Ok(usercredentials);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCredentialsComand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
