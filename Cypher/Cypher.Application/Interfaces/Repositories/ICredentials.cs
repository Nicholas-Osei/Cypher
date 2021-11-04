using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cypher.Domain.Entities.Catalog;

namespace Cypher.Application.Interfaces.Repositories
{
    
    public interface ICredentials
    {
        Task<IEnumerable<Credentials>> GetAll();
        Task<Credentials> Add(Credentials c);
        Task<int> InsertAsync(Credentials cred);
    }
}
