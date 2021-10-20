﻿using Cypher.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        Task<List<Product>> GetListAsync();

        Task<Product> GetByIdAsync(int productId);

        Task<int> InsertAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}