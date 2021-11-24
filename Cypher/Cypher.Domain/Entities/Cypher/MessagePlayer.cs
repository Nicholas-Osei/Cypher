namespace Cypher.Domain.Entities.Cypher
{
    public class MessagePlayer
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}