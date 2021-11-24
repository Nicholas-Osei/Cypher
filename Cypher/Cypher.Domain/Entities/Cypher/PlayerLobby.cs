namespace Cypher.Domain.Entities.Cypher
{
    public class PlayerLobby
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int LobbyId { get; set; }
        public Lobby Lobby { get; set; }
    }
}