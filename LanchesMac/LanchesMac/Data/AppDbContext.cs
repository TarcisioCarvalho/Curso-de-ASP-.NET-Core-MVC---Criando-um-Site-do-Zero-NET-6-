using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> carrinhoCompraItens { get ;set; }
    }
}
