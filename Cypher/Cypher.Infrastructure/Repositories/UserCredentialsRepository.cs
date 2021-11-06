using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;

namespace Cypher.Infrastructure.Repositories
{
    public class UserCredentialsRepository: IUserCredentialsRepository
    {
        private readonly IRepositoryAsync<UserCredentials> _repo;

        public UserCredentialsRepository(IRepositoryAsync<UserCredentials> repository)
        {
            _repo = repository;
        }

        public IQueryable<UserCredentials> UserCredentials => _repo.Entities;

        public async Task DeleteAsync(UserCredentials userCredential)
        {
            await _repo.DeleteAsync(userCredential);
        }

        public async Task<UserCredentials> GetByIdAsync(int userCredentialId)
        {
            return await _repo.Entities.Where(u => u.Id == userCredentialId).FirstOrDefaultAsync();
        }

        public async Task<List<UserCredentials>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(UserCredentials userCredential)
        {
            await _repo.AddAsync(userCredential);
            return userCredential.Id;
        }

        public Task UpdateAsync(UserCredentials userCredential)
        {
            throw new NotImplementedException();
        }
    }
}
