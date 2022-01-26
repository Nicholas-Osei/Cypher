using System;
using System.Threading.Tasks;
using Cypher.API.Controllers;
using Cypher.Application.Features.Questions.Commands.Create;
using Cypher.Application.Features.Questions.Commands.Delete;
using Cypher.Application.Features.Questions.Commands.Update;
using Cypher.Application.Features.Questions.Queries.GetAllQuestions;
using Cypher.Application.Features.Questions.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Cypher.Api.Controllers.v1
{
    public class QuestionController: BaseApiController<QuestionController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var questions = await _mediator.Send(new GetAllQuestionsQuery( pageNumber, pageSize));
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var questions = await _mediator.Send(new GetQuestionByIdQuery() { Id = id });
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateQuestionsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateQuestionCommand command)
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
            return Ok(await _mediator.Send(new DeleteQuestionCommand { Id = id }));
        }

       
    }
}
