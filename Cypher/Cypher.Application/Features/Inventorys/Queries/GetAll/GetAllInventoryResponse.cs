using System;
using System.Collections;
using System.Collections.Generic;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Inventorys.Queries.GetAll
{
    public class GetAllInventoryResponse
    {
        public int Id { get; set; }
        public virtual IEnumerable<Item> Items { get; set; }
        public GetAllInventoryResponse()
        {
        }
    }
}
