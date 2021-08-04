using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<ToDoContext>
    {
        public ToDoContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory())).AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"]/*, x=> x.MigrationsAssembly("OmerOzkan.ToDo.DataAccess")*/);

            return new ToDoContext(optionsBuilder.Options);
        }
    }
}
