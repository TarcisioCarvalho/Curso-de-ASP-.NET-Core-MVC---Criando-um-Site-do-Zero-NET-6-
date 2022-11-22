using LanchesMac.Data;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public string CarrinhoDeCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            ISession session = 
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context)
            {
                CarrinhoDeCompraId = carrinhoId,
            };
        }
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.carrinhoCompraItens
                .SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoDeCompraId == CarrinhoDeCompraId);

            if(carrinhoCompraItem != null)
            {
                carrinhoCompraItem.Quantidade++;
                _context.SaveChanges();
                return;
            }

            carrinhoCompraItem = new CarrinhoCompraItem()
            {
                CarrinhoDeCompraId = CarrinhoDeCompraId,
                Quantidade = 1,
                Lanche = lanche,
            };
            _context.carrinhoCompraItens.Add(carrinhoCompraItem);
            _context.SaveChanges();
        }
        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.carrinhoCompraItens
                .SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoDeCompraId == CarrinhoDeCompraId);

            var quantidadeLocal = 0;

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade <= 1)
                {
                    _context.Remove(carrinhoCompraItem);
                    _context.SaveChanges();
                    return quantidadeLocal;
                }

                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;

            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            return CarrinhoCompraItems ?? _context.carrinhoCompraItens
                .Where(c => c.CarrinhoDeCompraId == CarrinhoDeCompraId)
                .Include(s => s.Lanche)
                .ToList();
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.carrinhoCompraItens
                .Where(c => c.CarrinhoDeCompraId == CarrinhoDeCompraId);
            _context.carrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.carrinhoCompraItens
                .Where(c => c.CarrinhoDeCompraId == CarrinhoDeCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade)
                .Sum();
            return total;
        }
    }
}
