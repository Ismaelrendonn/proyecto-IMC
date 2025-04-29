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

namespace IMCAPI.Tests.Controllers
{
    public class CalculoIMCsControllerTests
    {
        private readonly Mock<CalculadoraIMCService> _mockCalculadora;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly CalculoIMCsController _controller;

        public CalculoIMCsControllerTests()
        {
            _mockCalculadora = new Mock<CalculadoraIMCService>();
            _mockContext = new Mock<ApplicationDbContext>();

            // Configurar DbSet mock
            var mockSet = new Mock<DbSet<CalculoIMC>>();
            _mockContext.Setup(c => c.CalculosIMC).Returns(mockSet.Object);

            _controller = new CalculoIMCsController(_mockCalculadora.Object, _mockContext.Object);
        }

        [Fact]
        public async Task Calcular_ConDatosValidos_RetornaOkConCalculoIMC()
        {
            // Arrange
            var request = new CalculoIMCRequest(70, 175);
            var expectedImc = 22.86;
            var expectedCategoria = "Normal";
            var expectedId = 1;
            var expectedFechaCalculo = DateTime.Now;

            _mockCalculadora.Setup(c => c.CalcularIMC(70, 1.75)).Returns((expectedImc, expectedCategoria));

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualCalculo = okResult.Value as dynamic;

            Assert.NotNull(actualCalculo);
            Assert.Equal(expectedImc, actualCalculo.imc);
            Assert.Equal(expectedCategoria, actualCalculo.categoria);
            //  Assert.Equal(expectedId, actualCalculo.id);  //  Verificar si es necesario (depende de la configuración de la base de datos en memoria)
            // Assert.IsType<DateTime>(actualCalculo.fechaCalculo); // Verificar el tipo de dato de la fecha

            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
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

            var mockSet = new Mock<DbSet<CalculoIMC>>();
            mockSet.As<IQueryable<CalculoIMC>>().Setup(m => m.Provider).Returns(calculosIMC.AsQueryable().Provider);
            mockSet.As<IQueryable<CalculoIMC>>().Setup(m => m.Expression).Returns(calculosIMC.AsQueryable().Expression);
            mockSet.As<IQueryable<CalculoIMC>>().Setup(m => m.ElementType).Returns(calculosIMC.AsQueryable().ElementType);
            mockSet.As<IQueryable<CalculoIMC>>().Setup(m => m.GetEnumerator()).Returns(calculosIMC.GetEnumerator());

            _mockContext.Setup(c => c.CalculosIMC).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetHistorial();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CalculoIMC>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualCalculos = okResult.Value as List<CalculoIMC>;

            Assert.NotNull(actualCalculos);
            Assert.Equal(2, actualCalculos.Count);
        }
    }
}