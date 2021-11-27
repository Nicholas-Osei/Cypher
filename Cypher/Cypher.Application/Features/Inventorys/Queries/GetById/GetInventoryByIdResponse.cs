using System;
using System.Collections.Generic;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Inventorys.Queries.GetById
{
    public class GetInventoryByIdResponse
    {
        public int Id { get; set; }
        public virtual IEnumerable<Item> Items { get; set; }
        public GetInventoryByIdResponse()
        {
        }
    }
}
