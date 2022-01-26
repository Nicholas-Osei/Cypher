using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Features.Players.Queries.GetAllPaged;
using Cypher.Application.Interfaces.Shared;
using Cypher.Domain.Entities.Cypher;
using Cypher.Web.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cypher.Web.Areas.Cypher.Pages
{
    public class MyPlayersModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUserService _userService;
        public List<GetAllPlayersResponse> Players;
        private IViewRenderService _viewRenderer;

        public MyPlayersModel(IMediator mediator, IAuthenticatedUserService userService, IViewRenderService viewRenderer)
        {
            _mediator = mediator;
            _userService = userService;
            _viewRenderer = viewRenderer;
        }

        public async Task OnGet()
        {
            //var response = await _mediator.Send(new GetAllPlayersQuery(null, null, null, _userService.UserId));
            var response = await _mediator.Send(new GetAllPlayersQuery(null, null, null));
            Players = response.Data;
        }
    }
}
