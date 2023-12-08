using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using API_Tiktok.Modelo;

namespace API_Tiktok.Controllers
{
    [ApiController]
    [Route("clima")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly List<WeatherForecast> Forecasts = new();
        private static int idCounter = 1;

        [HttpGet]
        [Route("listar")]
        public dynamic ListarPronosticos()
        {
            return new {
                success = true,
                message = "Lista de pronosticos",
                result = Forecasts
            };
        }

        [HttpPost]
        [Route("crear")]
        public dynamic CrearPronostico([FromBody] WeatherForecast newForecast)
        {
            if (newForecast == null)
            {
                return BadRequest();
            }

            newForecast.Id = idCounter++;
            Forecasts.Add(newForecast);
            
            return new
            {
                success = true,
                message = "Pronostico registrado",
                result = newForecast
            };
        }

        [HttpPut("{id}")]
        public dynamic Update(int id, [FromBody] WeatherForecast updatedForecast)
        {
            if (updatedForecast == null || updatedForecast.Id != id)
            {
                return BadRequest();
            }

            var forecast = Forecasts.FirstOrDefault(f => f.Id == id);
            if (forecast == null)
            {
                return NotFound();
            }

            forecast.Date = updatedForecast.Date;
            forecast.TemperatureC = updatedForecast.TemperatureC;
            forecast.Summary = updatedForecast.Summary;

            return new
            {
                success = true,
                message = "Pronostico actualizado",
                result = updatedForecast
            };
        }

        [HttpDelete("{id}")]
        [Authorize]
        public dynamic Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var validaToken = Jwt.validarToken(identity);

            if (!validaToken.success) {
                return validaToken;
            } 
            
            Usuario usuario = validaToken.result;

            if (usuario.rol != "administrador") {
                return new
                {
                    success = false,
                    message = "No tienes permiso",
                    result = ""
                };
            }
            var forecast = Forecasts.FirstOrDefault(f => f.Id == id);
            if (forecast == null)
            {
                return new
                {
                    success = false,
                    message = "Id incorrecto",
                    result = ""
                };
            }
            Forecasts.Remove(forecast);
            return new
            {
                success = true,
                message = "Se eliminó pronostico del clima",
                result = ""
            };
        }

    }
}
