using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IUserCredentialsRepository
    {
        IQueryable<UserCredential> UserCredential { get; }
        Task<List<UserCredential>> GetListAsync();
        Task<UserCredential> GetByIdAsync(int usercredential);
        Task<int> InsertAsync(UserCredential usercredential);
        Task UpdateAsync(UserCredential usercredential);
        Task DeleteAsync(UserCredential usercredential);
    }
}
