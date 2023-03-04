using Microsoft.EntityFrameworkCore;
namespace L01_2019CG606.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options) {

        }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set;}
        public DbSet<comentarios> comentarios { get; set; }
    }
}
