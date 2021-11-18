using Cypher.API.Controllers;
using Cypher.Application.Features.Items.Commands.Create;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Api.Controllers.v1
{
    public class ItemController : BaseApiController<ItemController>
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        //{
        //    var items = await _mediator.Send(new GetAllItemsQuery(pageNumber, pageSize));
        //    return Ok(items);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var item = await _mediator.Send(new GetItemByIdQuery() { Id = id });
        //    return Ok(item);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, UpdateItemCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(await _mediator.Send(command));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return Ok(await _mediator.Send(new DeleteItemCommand { Id = id }));
        //}
    }
}
