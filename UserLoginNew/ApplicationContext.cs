using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<Users> Users { get; set; } = null;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { 
    Database.EnsureCreated();
    }
    //protected override void onmodelcreating(modelbuilder modelbuilder)
    //{
    //    modelbuilder.entity<users>().hasdata(
    //        new users { id = 1, login = "tom", pass = "37" },
    //        new users { id = 2, login = "bob", pass = "41" },
    //        new users { id = 3, login = "sam", pass = "37" }
    //        );
    //}
}