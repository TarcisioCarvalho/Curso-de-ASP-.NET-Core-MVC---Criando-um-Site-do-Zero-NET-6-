using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
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
            //var lanches = _lancheRepository.Lanches;
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;

            

            //return View(_lancheRepository.Lanches);
            var lanchesViewModel = new LanchesListViewModel();
            lanchesViewModel.Lanches = _lancheRepository.Lanches;
            lanchesViewModel.CategoriaAtual = "Categoria Atual";
            var lanchesTotal = lanchesViewModel.Lanches.Count();
            ViewBag.Lanches = "O Total de Lanches é " + lanchesTotal;
            return View(lanchesViewModel);
        }
    }
}
