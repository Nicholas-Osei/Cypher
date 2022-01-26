using Cypher.Application.Features.Players.Queries.GetAllPaged;
using Cypher.Application.Interfaces.Shared;
using Cypher.Web.Abstractions;
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
                var mappedModel = _mapper.Map<List<GetAllPlayersResponse>>(response.Data);
                return PartialView("_ViewAll", mappedModel);
            }
            //return null;
            return null;
        }
    }
}
