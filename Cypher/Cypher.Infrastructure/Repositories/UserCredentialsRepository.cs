using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Cypher.Infrastructure.Repositories
{
    public class UserCredentialsRepository
    {
        private readonly IRepositoryAsync<Credentials> _repository;
        public UserCredentialsRepository(IRepositoryAsync<Credentials> repository)
        {
            _repository = repository;
        }

        public async Task<List<Credentials>> GetCredentialsListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
    }
}
