using System;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Messages.Queries.GetAll
{
    public class GetAllMessageResponse
    {
        //public GetAllMessageResponse()
        //{
        //}
        public int Id { get; set; }
        public string MessageText { get; set; }
        public Player Sender { get; set; }
    }
}
