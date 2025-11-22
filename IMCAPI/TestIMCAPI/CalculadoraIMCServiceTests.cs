using IMCAPI.Services;
using Xunit;
using System;

namespace IMCAPI.Tests.Services
{
    public class CalculadoraIMCServiceTests
    {
        private readonly CalculadoraIMCService _calculadora;

        public CalculadoraIMCServiceTests()
        {
            _calculadora = new CalculadoraIMCService();
        }

        // ============================================================
        //  PRUEBAS ORIGINALES (6 casos) - CORREGIDAS
        // ============================================================

        [Theory]
        [InlineData(70, 1.75, 22.86, "Normal")]
        [InlineData(50, 1.60, 19.53, "Normal")]
        [InlineData(90, 1.80, 27.78, "Sobrepeso")]
        [InlineData(45, 1.50, 20.00, "Normal")]
        [InlineData(110, 1.70, 38.06, "Obesidad grado II")] // corregido
        [InlineData(55, 1.50, 24.44, "Normal")]
        public void CalcularIMC_ConAlturaEnMetros_RetornaIMCYClasificacionCorrecta(
            double peso, double altura, double imcEsperado, string categoriaEsperada)
        {
            var (imc, categoria) = _calculadora.CalcularIMC(peso, altura);

            Assert.Equal(Math.Round(imcEsperado, 2), Math.Round(imc, 2));
            Assert.Equal(categoriaEsperada, categoria);
        }

        // ============================================================
        //  PRUEBAS DE VALIDACIÓN (4 casos)
        // ============================================================

        [Theory]
        [InlineData(0, 1.70)]
        [InlineData(-5, 1.70)]
        [InlineData(70, 0)]
        [InlineData(70, -1.60)]
        public void CalcularIMC_ConDatosInvalidos_LanzaArgumentException(double peso, double altura)
        {
            Assert.Throws<ArgumentException>(() => _calculadora.CalcularIMC(peso, altura));
        }

        // ============================================================
        //  **6 PRUEBAS NUEVAS INTEGRADAS - CORREGIDAS**
        // ============================================================

        [Fact]
        public void CalcularIMC_PesoMuyBajoPeroValido_RegresaCategoriaBajoPeso()
        {
            var (_, categoria) = _calculadora.CalcularIMC(30, 1.60);
            Assert.Equal("Bajo peso", categoria);
        }

        [Fact]
        public void CalcularIMC_LimiteInferiorNormal_RegresaNormal()
        {
            double peso = 49.2; // Ajustado para que IMC sea >= 18.5
            double altura = 1.63;

            var (_, categoria) = _calculadora.CalcularIMC(peso, altura);
            Assert.Equal("Normal", categoria);
        }

        [Fact]
        public void CalcularIMC_LimiteSobrepeso_RegresaSobrepeso()
        {
            double peso = 68.1; // Ajustado para que IMC sea >= 25
            double altura = 1.65;

            var (_, categoria) = _calculadora.CalcularIMC(peso, altura);
            Assert.Equal("Sobrepeso", categoria);
        }

        // corregida
        [Fact]
        public void CalcularIMC_AlturaMuyGrande_RegresaCategoriaCorrecta()
        {
            var (_, categoria) = _calculadora.CalcularIMC(100, 2.20);
            Assert.Equal("Normal", categoria);
        }

        [Fact]
        public void CalcularIMC_ResultadoConMasDecimales_RedondeoCorrecto()
        {
            var (imc, _) = _calculadora.CalcularIMC(73, 1.73);
            Assert.Equal(24.39, Math.Round(imc, 2));
        }

        // corregida
        [Fact]
        public void CalcularIMC_AlturaPositivaMuyPequena_NoLanzaExcepcionYRegresaCategoria()
        {
            var (_, categoria) = _calculadora.CalcularIMC(70, 0.30);
            Assert.Equal("Obesidad grado III", categoria);
        }
    }
}
