using Microsoft.EntityFrameworkCore;
namespace L01_2019CG606.Models
{
    public class comentariosContext : DbContext
    {
        public comentariosContext(DbContextOptions<comentariosContext> options) : base(options)
        {

        }
    }
}
