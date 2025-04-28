/*using IMCAPI.Data;
using IMCAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoIMCsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalculoIMCsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalculoIMC>>> GetCalculoIMC()
        {
            return await _context.CalculosIMC.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalculoIMC>> GetCalculoIMC(int id)
        {
            var calculoIMC = await _context.CalculosIMC.FindAsync(id);
            return calculoIMC ?? (ActionResult<CalculoIMC>)NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CalculoIMC>> PostCalculoIMC(CalculoIMC calculoIMC)
        {
            // Cálculo automático del IMC
            double alturaM = calculoIMC.Altura / 100;
            calculoIMC.ResultadoIMC = Math.Round(calculoIMC.Peso / (alturaM * alturaM), 2);
            calculoIMC.Categoria = calculoIMC.ResultadoIMC switch
            {
                < 18.5 => "Bajo peso",
                < 25 => "Normal",
                < 30 => "Sobrepeso",
                _ => "Obesidad"
            };

            _context.CalculosIMC.Add(calculoIMC);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCalculoIMC), new { id = calculoIMC.Id }, calculoIMC);
        }

        // ... otros métodos (PUT, DELETE) aquí
    }
}*/
/*using IMCAPI.Data;
using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> CalcularIMC([FromBody] CalculoIMC request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var (valor, clasificacion) = _calculadora.CalcularIMC(request.Peso, request.Altura);

            var calculo = new CalculoIMC
            {
                Peso = request.Peso,
                Altura = request.Altura,
                ResultadoIMC = valor,
                Categoria = clasificacion,
                FechaCalculo = DateTime.UtcNow
            };

            _context.CalculosIMC.Add(calculo);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Valor = valor,
                Clasificacion = clasificacion,
                Id = calculo.Id
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalculoIMC>>> GetHistorial()
    {
        return await _context.CalculosIMC.ToListAsync();
    }
}*/
using IMCAPI.Data;
using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace IMCAPI.Controllers {




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

        /*[HttpPost]
        public async Task<IActionResult> CalcularIMC([FromBody] CalculoIMCRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var (imc, categoria) = _calculadora.CalcularIMC(request.Peso, request.Altura);

                var calculo = new CalculoIMC
                {
                    Peso = request.Peso,
                    Altura = request.Altura,
                    ResultadoIMC = imc,
                    Categoria = categoria,
                    FechaCalculo = DateTime.UtcNow
                };

                _context.CalculosIMC.Add(calculo);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    imc,
                    categoria,
                    Id = calculo.Id
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        [HttpPost]
        [Route("Calcular")] // Opcional: puedes usar esto si quieres mantener ambos endpoints
        public async Task<IActionResult> Calcular([FromBody] CalculoIMCRequest request)
        {
            // Validación del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Cálculo del IMC
                var (imc, categoria) = _calculadora.CalcularIMC(request.Peso, request.AlturaCm);

                // Creación del registro en base de datos
                var calculo = new CalculoIMC
                {
                    Peso = request.Peso,
                    Altura = request.AlturaCm,
                    ResultadoIMC = imc,
                    Categoria = categoria,
                    FechaCalculo = DateTime.UtcNow
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
