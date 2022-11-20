using LanchesMac.Data;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public string CarrinhoCompraId { get; set; }
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
                CarrinhoCompraId = carrinhoId,
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.carrinhoCompraItens
                .SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoDeCompraId == CarrinhoCompraId);

            if(carrinhoCompraItem != null)
            {
                carrinhoCompraItem.Quantidade++;
                _context.SaveChanges();
                return;
            }

            carrinhoCompraItem = new CarrinhoCompraItem()
            {
                CarrinhoDeCompraId = CarrinhoCompraId,
                Quantidade = 1,
                Lanche = lanche,
            };
            _context.carrinhoCompraItens.Add(carrinhoCompraItem);
            _context.SaveChanges();
        }
    }
}
