using Microsoft.EntityFrameworkCore;
namespace L01_2019CG606.Models
{
    public class publicacionesContext : DbContext
    {
        public publicacionesContext(DbContextOptions<publicacionesContext> options) : base(options)
        {

        }
    }
}
