using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        IQueryable<Question> questions { get; }
        Task<List<Question>> GetListAsync();
        Task<Question> GetByIdAsync(int playerId);
        Task<int> InsertAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(Question question);
    }
}
