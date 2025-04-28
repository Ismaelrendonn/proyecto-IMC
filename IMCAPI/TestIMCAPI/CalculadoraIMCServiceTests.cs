// CalculadoraIMCServiceTests.cs
using IMCAPI.Services;
using Xunit;

namespace IMCAPI.Tests.Services
{
    public class CalculadoraIMCServiceTests
    {
        private readonly CalculadoraIMCService _calculadora;

        public CalculadoraIMCServiceTests()
        {
            _calculadora = new CalculadoraIMCService();
        }

        [Theory]
        [InlineData(70, 1.75, 22.86, "Normal")] // peso, altura(m), IMC esperado, categoría
        [InlineData(50, 1.65, 18.37, "Bajo peso")]
        [InlineData(90, 1.80, 27.78, "Sobrepeso")]
        [InlineData(100, 1.70, 34.60, "Obesidad grado I")]
        [InlineData(120, 1.75, 39.18, "Obesidad grado II")]
        [InlineData(150, 1.80, 46.30, "Obesidad grado III")]
        public void CalcularIMC_ConDatosValidos_RetornaIMCYClasificacionCorrecta(
            double peso, double altura, double imcEsperado, string categoriaEsperada)
        {
            // Act
            var (imc, categoria) = _calculadora.CalcularIMC(peso, altura);

            // Assert
            Assert.Equal(imcEsperado, Math.Round(imc, 2));
            Assert.Equal(categoriaEsperada, categoria);
        }

        [Theory]
        [InlineData(70, 175, 22.86, "Normal")] // peso, altura(cm), IMC esperado, categoría
        public void CalcularIMC_ConAlturaEnCentimetros_RetornaIMCYClasificacionCorrecta(
            double peso, double alturaCm, double imcEsperado, string categoriaEsperada)
        {
            // Act
            var (imc, categoria) = _calculadora.CalcularIMC(peso, alturaCm, alturaEnMetros: false);

            // Assert
            Assert.Equal(imcEsperado, Math.Round(imc, 2));
            Assert.Equal(categoriaEsperada, categoria);
        }

        [Theory]
        [InlineData(0, 1.75)] // peso cero
        [InlineData(-50, 1.75)] // peso negativo
        [InlineData(70, 0)] // altura cero
        [InlineData(70, -1.75)] // altura negativa
        public void CalcularIMC_ConDatosInvalidos_LanzaArgumentException(double peso, double altura)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculadora.CalcularIMC(peso, altura));
        }
    }
}
