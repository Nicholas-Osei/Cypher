using System;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        IQueryable<Message> Messages { get; }
        Task<int> InsertAsync(Message message);
        Task<Message> GetByIdAsync(int messageId);
        Task DeleteAsync(Message message);
        Task UpdateAsync(Message message);
    }
}
