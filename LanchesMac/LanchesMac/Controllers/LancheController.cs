using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            var lanches = _lancheRepository.Lanches;
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;

            var lanchesTotal = lanches.Count();

            ViewBag.Lanches = "O Total de Lanches é " + lanchesTotal;

            return View(_lancheRepository.Lanches);
        }
    }
}
