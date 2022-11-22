using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
