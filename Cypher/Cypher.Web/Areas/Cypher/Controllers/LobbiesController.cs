using Cypher.Domain.Entities.Cypher;
using Cypher.Application.Features.Lobbies.Queries;
using Cypher.Application.Features.Lobbies.Queries.GetById;
using Cypher.Application.Features.Players.Queries.GetAllPaged;
using Cypher.Web.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Web.Areas.Cypher.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cypher.Application.Features.Lobbies.CMDs.Create;
using Cypher.Application.Features.Lobbies.CMDs.Delete;
using Cypher.Application.Features.Lobbies.CMDs.Update;

namespace Cypher.Web.Areas.Cypher.Controllers
{
    [Area("Cypher")]
    public class LobbiesController : BaseController<LobbiesController>
    {
        // GET: LobbiesController
        public IActionResult Index()
        {
            var model = new Lobby();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllLobbiesQuery(null, null));
            if (response.Succeeded)
            {
                var mappedModel = _mapper.Map<List<LobbyViewModel>>(response.Data);
                return PartialView("_ViewAll", mappedModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var playersResponse = await _mediator.Send(new GetAllPlayersQuery(null, null, null));
            if (id == 0)
            {
                var lobbyViewModel = new LobbyViewModel();
                if (playersResponse.Succeeded)
                {
                    var playerViewModel = _mapper.Map<List<Player>>(playersResponse.Data);
                    lobbyViewModel.AllPlayers = new SelectList(playerViewModel, nameof(Player.Id), nameof(Player.Name), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", lobbyViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetLobbyByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var lobbyViewModel = _mapper.Map<LobbyViewModel>(response.Data);
                    if (playersResponse.Succeeded)
                    {
                        var playerViewModel = _mapper.Map<List<Player>>(playersResponse.Data);
                        lobbyViewModel.AllPlayers = new SelectList(playerViewModel, nameof(Player.Id), nameof(Player.Name), null, null);
                    }
                    return new JsonResult(new { IsValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", lobbyViewModel) });
                }
                return null;
            }
        }

        public async Task<JsonResult> OnPostCreateOrEdit(int id, LobbyViewModel lobby)
        {
            // Create
            if (id == 0)
            {
                var createLobbyCommand = _mapper.Map<CreateLobbyCommand>(lobby);
                var result = await _mediator.Send(createLobbyCommand);
                if (result.Succeeded)
                {
                    id = result.Data;
                    _notify.Success($"Lobby with ID { result.Data } Created.");
                }
                else
                {
                    _notify.Error(result.Message);
                }
            }
            else
            {
                // Update with update command
                var updateLobbyCommmand = _mapper.Map<UpdateLobbyCommand>(lobby);
                var result = await _mediator.Send(updateLobbyCommmand);
                if (result.Succeeded)
                {
                    _notify.Information($"Lobby with ID { result.Data } has been updated.");
                }
            }

            var response = await _mediator.Send(new GetAllLobbiesQuery(null, null));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<LobbyViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteLobbyCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Lobby with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllLobbiesQuery(null, null));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<LobbyViewModel>>(response.Data);
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
