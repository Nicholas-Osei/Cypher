using Cypher.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Api.Controllers.v1
{
    public class InventoryController : BaseApiController<InventoryController>
    {
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var inventory = await _mediator.Send(new GetInventoryByIdQuery() { Id = id });
        //    return Ok(inventory);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post(CreateInventoryCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, UpdateInventoryCommand command)
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
        //    return Ok(await _mediator.Send(new DeleteInventoryCommand { Id = id }));
        //}
    }
}
