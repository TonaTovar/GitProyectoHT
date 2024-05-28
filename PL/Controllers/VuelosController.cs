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
                client.BaseAddress = new Uri("https://localhost:7240/api/Vuelos/");

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ML.Vuelos>>();
                    readTask.Wait();

                    var jsonResult = readTask.Result;

                    if(jsonResult != null)
                    {
                        ML.Vuelos vuelos = new ML.Vuelos();
                        vuelos.aerolinia = new ML.Aerolinea();
                        vuelos.ListVuelos = new List<ML.Vuelos>();
                        var result1 = BL.Aerolinea.GetAll();
                        vuelos.aerolinia.ListAerolineas = result1.Item3;
                        vuelos.ListVuelos = jsonResult;

                        return View(vuelos);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
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

            if (IdVuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7240/api/Vuelos/");

                    var responseTask = client.GetAsync("GetById?IdVuelo=" + IdVuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Vuelos>();
                        readTask.Wait();

                        var jsonResult = readTask.Result;

                        if (jsonResult != null)
                        {
                            vuelos = readTask.Result;
                            var result2 = BL.Aerolinea.GetAll();
                            vuelos.aerolinia.ListAerolineas = result2.Item3;
                            return View(vuelos);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
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

                    var responseTask = client.PutAsJsonAsync<ML.Vuelos>("Update", vuelo);
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
