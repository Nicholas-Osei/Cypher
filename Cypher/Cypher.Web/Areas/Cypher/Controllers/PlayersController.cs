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
    }
}
