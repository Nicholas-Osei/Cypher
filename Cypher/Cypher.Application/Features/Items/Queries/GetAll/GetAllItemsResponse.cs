using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Features.Items.Queries
{
    public class GetAllItemsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
        //public Inventory Inventory { get; set; }
    }
}