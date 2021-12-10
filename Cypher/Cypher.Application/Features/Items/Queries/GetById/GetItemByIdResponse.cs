using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Items.Queries.GetById
{
    public class GetItemByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}