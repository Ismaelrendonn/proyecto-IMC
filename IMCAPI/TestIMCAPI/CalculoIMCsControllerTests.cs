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
        // -----------------------------
        // 1. Prueba original
        // -----------------------------
        [Fact]
        public async Task Calcular_IMC_DevuelveResultadoCorrecto()
        {
            var mockService = new Mock<CalculadoraIMCService>();
            mockService
                .Setup(s => s.CalcularIMC(It.IsAny<double>(), It.IsAny<double>()))
                .Returns((double peso, double altura) =>
                    (peso / (altura * altura), "Normal"));

            var controller = new CalculoIMCsController(mockService.Object);

            var request = new CalculoIMCRequest(70, 175);

            var result = await controller.Calcular(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultadoIMC = okResult.Value;

            var imc = (double)resultadoIMC.GetType().GetProperty("imc").GetValue(resultadoIMC, null);
            var categoria = (string)resultadoIMC.GetType().GetProperty("categoria").GetValue(resultadoIMC, null);

            Assert.Equal(22.86, imc, 2);
            Assert.Equal("Normal", categoria);
        }

        // -----------------------------
        // 2. Request nulo debe dar BadRequest
        // -----------------------------
        [Fact]
        public async Task Calcular_RequestNulo_RegresaBadRequest()
        {
            var mockService = new Mock<CalculadoraIMCService>();
            var controller = new CalculoIMCsController(mockService.Object);

            CalculoIMCRequest request = null;

            var result = await controller.Calcular(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // -----------------------------
        // 3. Altura inválida debe dar BadRequest
        // -----------------------------
        [Fact]
        public async Task Calcular_AlturaInvalida_RegresaBadRequest()
        {
            var mockService = new Mock<CalculadoraIMCService>();
            var controller = new CalculoIMCsController(mockService.Object);

            var request = new CalculoIMCRequest(70, 0);

            var result = await controller.Calcular(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // -----------------------------
        // 4. Peso inválido debe dar BadRequest
        // -----------------------------
        [Fact]
        public async Task Calcular_PesoInvalido_RegresaBadRequest()
        {
            var mockService = new Mock<CalculadoraIMCService>();
            var controller = new CalculoIMCsController(mockService.Object);

            var request = new CalculoIMCRequest(-10, 170);

            var result = await controller.Calcular(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // -----------------------------
        // 5. Categoría distinta (Sobrepeso)
        // -----------------------------
        [Fact]
        public async Task Calcular_CategoriaSobrepeso_RegresaValorCorrecto()
        {
            var mockService = new Mock<CalculadoraIMCService>();
            mockService
                .Setup(s => s.CalcularIMC(It.IsAny<double>(), It.IsAny<double>()))
                .Returns((28.5, "Sobrepeso"));

            var controller = new CalculoIMCsController(mockService.Object);
            var request = new CalculoIMCRequest(90, 175);

            var result = await controller.Calcular(request);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = ok.Value;

            // Leer propiedades con reflexión (case-insensitive)
            var type = value.GetType();
            var imcProp = type.GetProperty("imc") ?? type.GetProperty("IMC");
            var categoriaProp = type.GetProperty("categoria") ?? type.GetProperty("Categoria");

            var imc = (double)imcProp.GetValue(value);
            var categoria = (string)categoriaProp.GetValue(value);

            Assert.Equal(28.5, imc);
            Assert.Equal("Sobrepeso", categoria);
        }

        // -----------------------------
        // 6. Verificar que el servicio se invoque una vez
        // -----------------------------
        [Fact]
        public async Task Calcular_VerificaQueServicioSeaInvocado()
        {
            var mockService = new Mock<CalculadoraIMCService>();
            mockService
                .Setup(s => s.CalcularIMC(It.IsAny<double>(), It.IsAny<double>()))
                .Returns((25.0, "Normal"));

            var controller = new CalculoIMCsController(mockService.Object);
            var request = new CalculoIMCRequest(70, 167);

            await controller.Calcular(request);

            mockService.Verify(
                s => s.CalcularIMC(It.IsAny<double>(), It.IsAny<double>()),
                Times.Once
            );
        }
    }
}
