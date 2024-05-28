using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VuelosController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Vuelos vuelos = new ML.Vuelos();
            return View(vuelos);
        }
        [HttpGet]
        public IActionResult Form(int id_Vuelo)
        {
            ML.Vuelos vuelos = new ML.Vuelos();
            return View(vuelos);
        }
    }
}
