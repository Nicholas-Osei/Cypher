using Cypher.Application.Features.Players.Commands.Create;
using Cypher.Application.Features.Players.Commands.Delete;
using Cypher.Application.Features.Players.Commands.Update;
using Cypher.Application.Features.Players.Queries.GetAllPaged;
using Cypher.Application.Features.Players.Queries.GetById;
using Cypher.Application.Interfaces.Shared;
using Cypher.Web.Abstractions;
using Cypher.Web.Areas.Cypher.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Web.Areas.Cypher.Controllers
{
    [Area("Cypher")]
    public class PlayersController : BaseController<PlayersController>
    {
        private readonly IAuthenticatedUserService _userService;
        public PlayersController(IAuthenticatedUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllPlayersQuery(null, null, null, _userService.UserId));
            
            //Players = response.Data;

            if (response.Succeeded)
            {
                var mappedModel = _mapper.Map<List<PlayerViewModel>>(response.Data);
                return PartialView("_ViewAll", mappedModel);
            }
            //return null;
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var playerViewModel = new PlayerViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", playerViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetPlayerByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var playerViewModel = _mapper.Map<PlayerViewModel>(response.Data);
                    return new JsonResult(new { IsValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", playerViewModel) });
                }
                return null;
            }
        }

        public async Task<JsonResult> OnPostCreateOrEdit(int id, PlayerViewModel player)
        {
            // Create
            if (id == 0)
            {
                var createPlayerCommand = _mapper.Map<CreatePlayerCommand>(player);
                var result = await _mediator.Send(createPlayerCommand);
                if (result.Succeeded)
                {
                    id = result.Data;
                    _notify.Success($"Player with ID { result.Data } Created.");
                }
                else
                {
                    _notify.Error(result.Message);
                }
            }
            else
            {
                // Update with update command
                var updatePlayerCommmand = _mapper.Map<UpdatePlayerCommand>(player);
                var result = await _mediator.Send(updatePlayerCommmand);
                if (result.Succeeded)
                {
                    _notify.Information($"Player with ID { result.Data } has been updated.");
                }
            }

            var response = await _mediator.Send(new GetAllPlayersQuery(null, null, null, _userService.UserId));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<PlayerViewModel>>(response.Data);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                _notify.Error(response.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeletePlayerCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Lobby with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllPlayersQuery(null, null, null, _userService.UserId));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<PlayerViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}
