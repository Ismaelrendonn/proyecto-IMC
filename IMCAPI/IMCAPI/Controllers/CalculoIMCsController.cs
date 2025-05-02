using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoIMCsController : ControllerBase
    {
        private readonly CalculadoraIMCService _calculadoraIMCService;

        // Constructor con inyección de dependencias
        public CalculoIMCsController(CalculadoraIMCService calculadoraIMCService)
        {
            _calculadoraIMCService = calculadoraIMCService;
        }

        // Método Calcular IMC
        [HttpPost("calcular")]
        public async Task<IActionResult> Calcular([FromBody] CalculoIMCRequest request)
        {
            if (request == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            // Validar si Peso y AlturaCm no son nulos
            if (!request.Peso.HasValue || !request.AlturaCm.HasValue)
            {
                return BadRequest("Peso y altura son obligatorios.");
            }

            // Convertir altura a metros
            double alturaEnMetros = request.AlturaCm.Value / 100;

            // Imprimir para verificar los valores que están llegando
            Console.WriteLine($"Peso recibido: {request.Peso.Value}, Altura recibida: {request.AlturaCm.Value} cm");

            // Lógica del cálculo (peso ya es un valor double)
            var resultadoIMC = _calculadoraIMCService.CalcularIMC(request.Peso.Value, alturaEnMetros);

            // Imprimir el resultado del cálculo del IMC
            Console.WriteLine($"IMC calculado: {resultadoIMC.imc}, Categoría: {resultadoIMC.categoria}");

            // Verificar si el resultadoIMC es válido
            if (resultadoIMC.imc <= 0)
            {
                return BadRequest("El cálculo del IMC no es válido.");
            }

            // Devuelve el resultado en el body
            return Ok(new { imc = resultadoIMC.imc, categoria = resultadoIMC.categoria });
        }
    }
}
