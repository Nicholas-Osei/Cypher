using Cypher.API.Controllers;
using Cypher.Application.Features.Lobbies.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Api.Controllers.v1
{
    public class LobbyController : BaseApiController<LobbyController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int? pageNumber, int? pageSize)
        {
            var lobbies = await _mediator.Send(new GetAllLobbiesQuery(pageNumber, pageSize));
            return Ok(lobbies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player = await _mediator.Send(new GetPlayerByIdQuery() { Id = id });
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePlayerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePlayerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePlayerCommand { Id = id }));
        }
    }
}
