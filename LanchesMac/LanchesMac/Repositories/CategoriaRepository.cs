using LanchesMac.Data;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class CategoriaRepository:ICategoriaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Categoria> Categorias => _appDbContext.Categorias;
    }
}
