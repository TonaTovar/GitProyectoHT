﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
            
        public IActionResult GetAll()
        {
            ML.Vuelos vuelos= new ML.Vuelos();
            var result  = BL.Vuelos.GetAll();
            vuelos.ListVuelos = new List<ML.Vuelos>();
            if (result.Item1)
            {
                
                vuelos.ListVuelos = result.Item3;
                return Ok(result.Item3);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add ([FromBody]ML.Vuelos? vuelos)
        {
            var result = BL.Vuelos.Add(vuelos);
            if (result.Item1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int IdVuelo)
        {
            var result = BL.Vuelos.GetById(IdVuelo);
            if (result.Item1)
            {
                return Ok(result.Item3);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]ML.Vuelos? vuelos)
        {
            var result = BL.Vuelos.Update(vuelos);
            if (result.Item1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int IdVuelo)
        {
            var result = BL.Vuelos.Delete(IdVuelo);
            if (result.Item1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
