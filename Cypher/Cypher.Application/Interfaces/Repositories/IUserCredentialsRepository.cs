using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IUserCredentialsRepository
    {
        IQueryable<UserCredentials> UserCredentials { get; }
        Task<List<UserCredentials>> GetListAsync();
        Task<UserCredentials> GetByIdAsync(int usercredentialsId);
        Task<int> InsertAsync(UserCredentials usercredentials);
        Task UpdateAsync(UserCredentials usercredentials);
        Task DeleteAsync(UserCredentials usercredentials);
    }
}
