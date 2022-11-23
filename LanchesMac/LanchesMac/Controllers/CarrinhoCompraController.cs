using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItems = itens;
            var CarrinhoCompraViewModel = new CarrinhoCompraViewModel()
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(CarrinhoCompraViewModel);
        }

        public RedirectToActionResult AdicionarNoCarrinhoCompra(int idLanche)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                .FirstOrDefault(l => l.LancheId == idLanche);

            if (lancheSelecionado != null) _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoverDoCarrinho(int idLanche)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                .FirstOrDefault(l => l.LancheId == idLanche);

            if (lancheSelecionado != null) _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);

            return RedirectToAction(nameof(Index));
        }
    }
}
