using Microsoft.EntityFrameworkCore;

namespace desafioBoiSaude.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
