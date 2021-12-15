using System;
using System.Threading.Tasks;
using Cypher.API.Controllers;
using Cypher.Application.Features.Items.Queries.GetById;
using Cypher.Application.Features.Messages.Commands.Create;
using Cypher.Application.Features.Messages.Commands.Delete;
using Cypher.Application.Features.Messages.Commands.Update;
using Cypher.Application.Features.Messages.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Cypher.Api.Controllers.v1
{
    public class MessageController: BaseApiController<MessageController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int? pageNumber, int? pageSize)
        {
            var messages = await _mediator.Send(new GetAllMessagesQuery(pageNumber, pageSize));
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var messages = await _mediator.Send(new GetItemByIdQuery() { Id = id });
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMessageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateMessageCommand command)
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
            return Ok(await _mediator.Send(new DeleteMessageCommand { Id = id }));
        }
    }
}
