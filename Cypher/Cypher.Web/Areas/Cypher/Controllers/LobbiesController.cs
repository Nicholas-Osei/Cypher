using Cypher.Domain.Entities.Cypher;
using Cypher.Application.Features.Lobbies.Queries;
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
                var mappedModel = _mapper.Map<List<Lobby>>(response.Data);
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
                    lobbyViewModel.AllPlayers = new SelectList(playerViewModel, nameof(Player.Id)
                }
            }
        }

        // GET: LobbiesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LobbiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LobbiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LobbiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LobbiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LobbiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LobbiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
