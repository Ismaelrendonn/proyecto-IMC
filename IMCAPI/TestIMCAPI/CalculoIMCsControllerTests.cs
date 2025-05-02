using IMCAPI.Controllers;
using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestIMCAPI
{
    public class CalculoIMCsControllerTests
    {
        // Prueba para el cálculo del IMC
        [Fact]
        public async Task Calcular_IMC_DevuelveResultadoCorrecto()
        {
            // Arrange: Crear un mock del servicio CalculadoraIMCService
            var mockService = new Mock<CalculadoraIMCService>();
            mockService
                .Setup(s => s.CalcularIMC(It.IsAny<double>(), It.IsAny<double>()))
                .Returns((double peso, double altura) =>
                    (peso / (altura * altura), "Normal"));

            // Crear el controlador con el servicio mockeado
            var controller = new CalculoIMCsController(mockService.Object);

            // Crear un objeto de solicitud
            var request = new CalculoIMCRequest(70, 175);  // Peso en kg y altura en cm

            // Act: Llamar al método Calcular
            var result = await controller.Calcular(request);

            // Assert: Verificar que el resultado es correcto
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultadoIMC = okResult.Value;

            // Acceder a las propiedades de la tupla anónima utilizando reflexión
            var imc = (double)resultadoIMC.GetType().GetProperty("imc").GetValue(resultadoIMC, null);
            var categoria = (string)resultadoIMC.GetType().GetProperty("categoria").GetValue(resultadoIMC, null);

            // Verificar que el IMC calculado es correcto (redondeado a 2 decimales)
            Assert.Equal(22.86, imc, 2);

            // Verificar la categoría
            Assert.Equal("Normal", categoria);
        }
    }
}
