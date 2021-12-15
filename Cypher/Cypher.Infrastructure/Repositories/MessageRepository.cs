using System;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;

namespace Cypher.Infrastructure.Repositories
{
    public class MessageRepository:IMessageRepository
    {
        private readonly IRepositoryAsync<Message> _repo;
        public MessageRepository(IRepositoryAsync<Message> repo)
        {
            _repo = repo;
        }

        public IQueryable<Message> Messages => _repo.Entities;

        public async Task DeleteAsync(Message message)
        {
            await _repo.DeleteAsync(message);
        }

        public async Task<Message> GetByIdAsync(int messageId)
        {
            return await _repo.Entities.Where(i => i.Id == messageId).FirstOrDefaultAsync();
        }

        public async Task<int> InsertAsync(Message message)
        {
            await _repo.AddAsync(message);

            return message.Id;
        }

        public async Task UpdateAsync(Message message)
        {
            await _repo.UpdateAsync(message);
        }
    }
}
