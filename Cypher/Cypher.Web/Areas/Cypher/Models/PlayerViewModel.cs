using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Web.Areas.Cypher.Models
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
