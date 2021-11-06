using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Microsoft.EntityFrameworkCore;

namespace Cypher.Infrastructure.Repositories
{
    public class UserCredentialRepository:IUserCredentialsRepository
    {
        private readonly IRepositoryAsync<UserCredential> _repo;

        public UserCredentialRepository(IRepositoryAsync<UserCredential> repository)
        {
            _repo = repository;
        }


        public IQueryable<UserCredential> UserCredential => _repo.Entities;

        public async Task DeleteAsync(UserCredential usercrfedential)
        {
            await _repo.DeleteAsync(usercrfedential);
        }

        public async Task<UserCredential> GetByIdAsync(int productId)
        {
            return await _repo.Entities.Where(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<List<UserCredential>> GetListAsync()
        {
            return await _repo.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(UserCredential usercredential)
        {
            await _repo.AddAsync(usercredential);
            return usercredential.Id;
        }

        public Task UpdateAsync(UserCredential player)
        {
            throw new NotImplementedException();
        }
    }
}

