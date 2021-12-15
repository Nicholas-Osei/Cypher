using System.Reflection;
using AutoMapper;
using Cypher.Application.Interfaces.CacheRepositories;
using Cypher.Application.Interfaces.Contexts;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Infrastructure.CacheRepositories;
using Cypher.Infrastructure.DbContexts;
using Cypher.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cypher.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {
        public static void AddPersistenceContexts (this IServiceCollection services, IConfiguration configuration) {
            services.AddAutoMapper (Assembly.GetExecutingAssembly ());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext> ();
        }

        public static void AddRepositories (this IServiceCollection services) {
            #region Repositories

            services.AddTransient (typeof (IRepositoryAsync<>), typeof (RepositoryAsync<>));
            services.AddTransient<IUserCredentialsRepository, UserCredentialRepository> ();
            services.AddTransient<IPlayerRepository, PlayerRepository> ();
            services.AddTransient<IPlayerCacheRepository, PlayerCacheRepository> ();
            services.AddTransient<IProductRepository, ProductRepository> ();
            services.AddTransient<IProductCacheRepository, ProductCacheRepository> ();
            services.AddTransient<IBrandRepository, BrandRepository> ();
            services.AddTransient<IBrandCacheRepository, BrandCacheRepository> ();
            services.AddTransient<ILogRepository, LogRepository> ();
            services.AddTransient<IUnitOfWork, UnitOfWork> ();
            services.AddTransient<IItemRepository, ItemRepository> ();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            #endregion Repositories
        }
    }
}