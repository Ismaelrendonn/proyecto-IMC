using IMCAPI.Data;
using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculoIMCsController : ControllerBase
    {
        private readonly CalculadoraIMCService _calculadora;
        private readonly ApplicationDbContext _context;

        public CalculoIMCsController(
            CalculadoraIMCService calculadora,
            ApplicationDbContext context)
        {
            _calculadora = calculadora;
            _context = context;
        }

        [HttpPost]
        [Route("Calcular")]
        public async Task<IActionResult> Calcular([FromBody] CalculoIMCRequest request)
        {
            // Validación del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Conversión de altura a metros
                var alturaEnMetros = request.AlturaCm / 100.0;

                // Cálculo del IMC
                var (imc, categoria) = _calculadora.CalcularIMC(request.Peso, alturaEnMetros);

                // Creación del registro en base de datos
                var calculo = new CalculoIMC
                {
                    Peso = request.Peso,
                    AlturaCm = request.AlturaCm, // Usar AlturaCm
                    ResultadoIMC = imc,
                    Categoria = categoria
                    // FechaCalculo se genera en la base de datos
                };

                _context.CalculosIMC.Add(calculo);
                await _context.SaveChangesAsync();

                // Respuesta con los resultados
                return Ok(new
                {
                    imc,
                    categoria,
                    Id = calculo.Id,
                    fechaCalculo = calculo.FechaCalculo
                });
            }
            catch (ArgumentException ex)
            {
                // Manejo de errores específicos del cálculo
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales (opcional)
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalculoIMC>>> GetHistorial()
        {
            return await _context.CalculosIMC.ToListAsync();
        }
    }
}