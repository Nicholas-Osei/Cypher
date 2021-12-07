using Cypher.API.Controllers;
using Cypher.Application.Features.Players.Commands.Create;
using Cypher.Application.Features.Players.Commands.Delete;
using Cypher.Application.Features.Players.Commands.Update;
using Cypher.Application.Features.Players.Queries.GetAllPaged;
using Cypher.Application.Features.Players.Queries.GetById;
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
        public async Task<IActionResult> GetAll(int? pageNumber, int? pageSize)
        {
            var players = await _mediator.Send(new GetAllPlayersQuery(pageNumber, pageSize));
            return Ok(players);
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

        [HttpPut("{playerId}/friends")]
        public async Task<IActionResult> Put(int playerId, UpdateFriendsCommand command)
        {
            if (playerId != command.PlayerId)
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

        [HttpDelete("{playerId}/friends/{friendId}")]
        public async Task<IActionResult> Delete(int playerId, int friendId)
        {
            return Ok(await _mediator.Send(new DeleteFriendCommand
            {
                PlayerId = playerId,
                FriendId = friendId
            }));
        }
    }
}
