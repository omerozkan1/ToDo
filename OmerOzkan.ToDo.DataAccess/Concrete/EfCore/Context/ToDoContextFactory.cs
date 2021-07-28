using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<ToDoContext>
    {
        public ToDoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-9TOE094;Database=ToDoDb;Trusted_Connection=True;MultipleActiveResultSets=true" /*, b => b.MigrationsAssembly("OmerOzkan.ToDo.Data")*/);

            return new ToDoContext(optionsBuilder.Options);
        }
    }
}
