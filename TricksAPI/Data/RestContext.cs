using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TricksAPI.Data.Entities;
using TricksAPI.Data.Dtos.Auth;

namespace TricksAPI.Data
{
    public class RestContext : IdentityDbContext<RestUser>
    {
        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=tcp:tricksapidbserver.database.windows.net,1433;Initial Catalog=TricksAPI_db;User Id=benas@tricksapidbserver;Password=makakis_528");
            optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB; Initial Catalog=TricksDB");
        }
    }
}
