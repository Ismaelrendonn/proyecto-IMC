using IMCAPI.Controllers;
using IMCAPI.Data;
using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IMCAPI.Tests.Controllers
{
    public class CalculoIMCsControllerTests
    {
        private readonly Mock<CalculadoraIMCService> _mockCalculadora;
        private readonly ApplicationDbContext _mockContext;
        private readonly CalculoIMCsController _controller;

        public CalculoIMCsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDb")
                            .Options;

            _mockContext = new ApplicationDbContext(options);
            _mockCalculadora = new Mock<CalculadoraIMCService>();
            _controller = new CalculoIMCsController(_mockCalculadora.Object, _mockContext);
        }

        [Fact]
        public async Task Calcular_ConDatosValidos_RetornaOkConCalculoIMC()
        {
            // Arrange
            var request = new CalculoIMCRequest(70, 175);
            var expectedImc = 22.86;
            var expectedCategoria = "Normal";

            _mockCalculadora.Setup(c => c.CalcularIMC(70, 1.75)).Returns((expectedImc, expectedCategoria));

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualCalculo = Assert.IsType<CalculoIMCResponse>(okResult.Value); // Ya no es dynamic

            Assert.NotNull(actualCalculo);
            Assert.Equal(expectedImc, actualCalculo.Imc);           // Propiedad con mayúscula
            Assert.Equal(expectedCategoria, actualCalculo.Categoria); // Propiedad con mayúscula
        }

        [Fact]
        public async Task Calcular_ConModeloInvalido_RetornaBadRequest()
        {
            // Arrange
            var request = new CalculoIMCRequest(-1, 175);
            _controller.ModelState.AddModelError("Peso", "El peso debe ser positivo");

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Calcular_ConExcepcionEnCalculadora_RetornaBadRequestConMensajeDeError()
        {
            // Arrange
            var request = new CalculoIMCRequest(70, 0);
            _mockCalculadora.Setup(c => c.CalcularIMC(70, 0)).Throws(new ArgumentException("Altura debe ser positiva"));

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Altura debe ser positiva", (badRequestResult.Value as string));
        }

        [Fact]
        public async Task GetHistorial_RetornaOkConListaDeCalculosIMC()
        {
            // Arrange
            var calculosIMC = new List<CalculoIMC>
            {
                new CalculoIMC { Id = 1, Peso = 70, AlturaCm = 175, ResultadoIMC = 22.86, Categoria = "Normal" },
                new CalculoIMC { Id = 2, Peso = 80, AlturaCm = 170, ResultadoIMC = 27.68, Categoria = "Sobrepeso" }
            };

            _mockContext.CalculosIMC.AddRange(calculosIMC);
            await _mockContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetHistorial();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CalculoIMC>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualCalculos = Assert.IsType<List<CalculoIMC>>(okResult.Value);

            Assert.NotNull(actualCalculos);
            Assert.Equal(2, actualCalculos.Count);
        }
    }
}
