namespace Cypher.Application.Features.Players.Queries.GetById
{
    public class GetPlayerByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}