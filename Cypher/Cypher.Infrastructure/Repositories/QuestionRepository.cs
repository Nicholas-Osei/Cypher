using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;

namespace Cypher.Infrastructure.Repositories
{
    public class QuestionRepository: IQuestionRepository
    {
        public QuestionRepository()
        {
        }
        private readonly IRepositoryAsync<Question> _repo;

        public QuestionRepository(IRepositoryAsync<Question> repository)
        {
            _repo = repository;
        }


        public IQueryable<Question> questions => _repo.Entities;


        public async Task DeleteAsync(Question usercredential)
        {
            await _repo.DeleteAsync(usercredential);
        }

        public async Task<Question> GetByIdAsync(int questionId)
        {
            return await _repo.Entities.Where(p => p.Id == questionId).FirstOrDefaultAsync();
        }

        public async Task<List<Question>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Question question)
        {
            await _repo.AddAsync(question);
            return question.Id;
        }

        public async Task UpdateAsync(Question question)
        {
            await _repo.UpdateAsync(question);
     
        }
    }
}

