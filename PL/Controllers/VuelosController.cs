using DL;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VuelosController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7240/api/Vuelo/");

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ML.Vuelos vuelos = new ML.Vuelos();
                    vuelos.aerolinia = new ML.Aerolinea();

                    var result1 = BL.Aerolinea.GetAll();
                    vuelos.aerolinia.ListAerolineas = result1.Item3;

                    return View(vuelos);
                }
                else
                {
                    ML.Vuelos vuelos = new ML.Vuelos();
                    vuelos.aerolinia = new ML.Aerolinea();
                    return View(vuelos);
                }
            }            
        }
        [HttpGet]
        public IActionResult Form(int IdVuelo)
        {
            ML.Vuelos vuelos = new ML.Vuelos();
            vuelos.aerolinia = new ML.Aerolinea();
            vuelos.aerolinia.ListAerolineas = new List<ML.Aerolinea>();

            var result1 = BL.Aerolinea.GetAll();
            vuelos.aerolinia.ListAerolineas = result1.Item3;

            if (IdVuelo != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelo/");

                    var responseTask = client.GetAsync("GetById?IdVuelo=" + IdVuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return View(vuelos);
                    }
                    else
                    {
                        return View(vuelos);
                    }
                }

            }
            else
            {
                return View(vuelos);

            }
        }

        [HttpPost]
        public IActionResult Form(ML.Vuelos vuelo)
        {
            if(vuelo.Id_Vuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelo/");

                    var responseTask = client.PostAsJsonAsync<ML.Vuelos>("Update", vuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El vuelo se actualizo correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El vuelo no se actualizo";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelo/");

                    var responseTask = client.PostAsJsonAsync<ML.Vuelos>("Add", vuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El vuelo se registro correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El vuelo no se agrego";
                        return PartialView("Modal");
                    }
                }
            }
        }
        public IActionResult Delete(int IdVuelo)
        {
            if (IdVuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:7240/api/Vuelo");

                    var responseTask = client.DeleteAsync("Delete?IdVuelo=" + IdVuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El vuelo se elimino correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El vuelo no se elimino";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                ViewBag.Text = "Hubo un error";
                return PartialView("Modal");
            }
        }
    }
}
