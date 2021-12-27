using Cypher.Domain.Entities.Cypher;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Web.Areas.Cypher.Models
{
    public class LobbyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LobbyAdminId { get; set; }
        public List<Player> Players{ get; set; }
        public SelectList AllPlayers { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
