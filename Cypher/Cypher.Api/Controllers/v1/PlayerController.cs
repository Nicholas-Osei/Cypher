using Cypher.API.Controllers;
using Cypher.Application.Features.Players.Commands.Create;
using Cypher.Application.Features.Players.Queries.GetAllPaged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Api.Controllers.v1
{
    public class PlayerController : BaseApiController<PlayerController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var players = await _mediator.Send(new GetAllPlayersQuery(pageNumber, pageSize));
            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePlayerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
