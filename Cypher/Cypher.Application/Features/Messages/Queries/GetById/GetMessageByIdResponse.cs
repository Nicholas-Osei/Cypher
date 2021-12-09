using System;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Messages.Queries.GetById
{
    public class GetMessageByIdResponse
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public virtual Player Player { get; set; }
    }
}
