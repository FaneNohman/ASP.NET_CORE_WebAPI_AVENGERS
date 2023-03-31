using Avengers.Models;
using Microsoft.EntityFrameworkCore;

namespace CORE_MVC_EXAM.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Avenger> Avengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
