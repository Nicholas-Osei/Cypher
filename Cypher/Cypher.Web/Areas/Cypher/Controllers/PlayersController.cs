using Cypher.Application.Features.Players.Queries.GetAllPaged;
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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            //var response = await _mediator.Send(new GetAllPlayersQuery(null, null, null, ));
            //if (response.Succeeded)
            //{
            //    var mappedModel = _mapper.Map<List<LobbyViewModel>>(response.Data);
            //    return PartialView("_ViewAll", mappedModel);
            //}
            //return null;
            return null;
        }
    }
}
