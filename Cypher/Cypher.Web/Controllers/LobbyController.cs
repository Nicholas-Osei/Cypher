using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Web.Controllers
{
    [Area("Cypher")]
    public class LobbyController : BaseController<LobbyController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            throw new NotImplementedException();
        }
    }
}
