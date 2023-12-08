using API_Tiktok.Modelo;
using API_Tiktok.Recursos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace API_Tiktok.Controllers
{
    [ApiController]
    [Route("Producto")]
    public class ProductoController : ControllerBase
    {

        [HttpGet]
        [Route("listar")]
        public dynamic ListarProductos() { 
            
            List<Parametro> parametros = new List<Parametro> { 
                new Parametro("@Estado","1")
            };
            DataTable tCategoria = DBDatos.Listar("Categoria_Listar", parametros);
            DataTable tProducto = DBDatos.Listar("Producto_Listar");

            string jsonCategoria = JsonConvert.SerializeObject(tCategoria);
            string jsonProducto = JsonConvert.SerializeObject(tProducto);

            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categoria = JsonConvert.DeserializeObject<List<Categoria>>(jsonCategoria),
                    producto = JsonConvert.DeserializeObject<List<Producto>>(jsonProducto)
                }

            };
        }

    }
}
