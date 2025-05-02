using IMCAPI.Services;
using Xunit;
using System;

namespace TestIMCAPI
{
    public class CalculadoraIMCServiceTests
    {
        private readonly CalculadoraIMCService _calculadora;

        public CalculadoraIMCServiceTests()
        {
            _calculadora = new CalculadoraIMCService();
        }

        [Theory]
        [InlineData(70, 1.75, 22.86, "Normal")]
        [InlineData(50, 1.65, 18.37, "Bajo peso")]
        [InlineData(90, 1.80, 27.78, "Sobrepeso")]
        [InlineData(100, 1.70, 34.60, "Obesidad grado I")]
        [InlineData(120, 1.75, 39.18, "Obesidad grado II")]
        [InlineData(150, 1.80, 46.30, "Obesidad grado III")]
        public void CalcularIMC_ConAlturaEnMetros_RetornaIMCYClasificacionCorrecta(
            double peso, double alturaEnMetros, double imcEsperado, string categoriaEsperada)
        {
            // Act
            var (imc, categoria) = _calculadora.CalcularIMC(peso, alturaEnMetros);

            // Assert
            Assert.Equal(imcEsperado, Math.Round(imc, 2));
            Assert.Equal(categoriaEsperada, categoria);
        }

        [Theory]
        [InlineData(0, 1.75)]
        [InlineData(-50, 1.75)]
        [InlineData(70, 0)]
        [InlineData(70, -1.75)]
        public void CalcularIMC_ConDatosInvalidos_LanzaArgumentException(double peso, double alturaEnMetros)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculadora.CalcularIMC(peso, alturaEnMetros));
        }
    }
}
