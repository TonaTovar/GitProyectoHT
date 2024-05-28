using DL;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
namespace PL.Controllers
{
    public class VuelosController : Controller
    {
        public IActionResult GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7240/api/Vuelos/");

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    var vuelos = JsonSerializer.Deserialize<List<ML.Vuelos>>(content);

                    ML.Vuelos vuelosModel = new ML.Vuelos();
                    vuelosModel.aerolinia = new ML.Aerolinea();
                    vuelosModel.ListVuelos = vuelos;

                    var result1 = BL.Aerolinea.GetAll();
                    if (result1.Item1)
                    {
                        vuelosModel.aerolinia.ListAerolineas = result1.Item3;
                    }

                    return View(vuelosModel);
                }
                else
                {
                    ML.Vuelos vuelos = new ML.Vuelos();
                    vuelos.aerolinia = new ML.Aerolinea();
                    vuelos.ListVuelos = new List<ML.Vuelos>();
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
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelos/");

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
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelos/");

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
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelos/");

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
        [HttpDelete]
        public IActionResult Delete(ML.Vuelos vuelos)
        {
            if (vuelos.Id_Vuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:7240/api/Vuelos/");

                    var responseTask = client.DeleteAsync("Delete?IdVuelo=" + vuelos.Id_Vuelo);

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
