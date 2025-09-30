using Microsoft.EntityFrameworkCore.Design;
using Repository;
using Microsoft.EntityFrameworkCore;

namespace Onions.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection")
                , b => b.MigrationsAssembly("Onions"));
            return new RepositoryContext(builder.Options);
        }
    }
}
