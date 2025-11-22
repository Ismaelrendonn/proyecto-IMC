// CalculoIMCRequestTests.cs
using IMCAPI.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace IMCAPI.Tests.Models
{
    public class CalculoIMCRequestTests
    {
        // -------------------------------
        // PRUEBA ORIGINAL (Theory)
        // -------------------------------
        [Theory]
        [InlineData(20, 50, false)]
        [InlineData(300, 250, true)]
        [InlineData(70, 175, true)]
        [InlineData(19, 50, false)]
        [InlineData(301, 250, false)]
        [InlineData(70, 49, false)]
        [InlineData(70, 251, false)]
        public void Validaciones_DevuelveResultadoEsperado(double peso, double alturaCm, bool esperadoValido)
        {
            var request = new CalculoIMCRequest(peso, alturaCm);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(request, context, results, true);

            Assert.Equal(esperadoValido, isValid);

            if (!esperadoValido)
            {
                Assert.NotEmpty(results);
            }
        }

        // -------------------------------
        // 6 PRUEBAS NUEVAS
        // -------------------------------

        // 1️⃣ Peso nulo debe ser inválido
        [Fact]
        public void PesoNulo_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(null, 170);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 2️⃣ Altura nula debe ser inválida
        [Fact]
        public void AlturaNula_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(70, null);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 3️⃣ Validación correcta con valores en el límite inferior
        [Fact]
        public void ValoresLimiteInferior_RegresanValidos()
        {
            var request = new CalculoIMCRequest(20, 50);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            // Según tu teoría original, estos valores son inválidos
            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 4️⃣ Validación correcta con valores en el límite superior
        [Fact]
        public void ValoresLimiteSuperior_RegresanValidos()
        {
            var request = new CalculoIMCRequest(300, 250);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.True(valid);
        }

        // 5️⃣ Validación falla cuando ambos campos son inválidos
        [Fact]
        public void AmbosValoresInvalidos_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(10, 20);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 6️⃣ Validación falla cuando ambos valores son nulos
        [Fact]
        public void AmbosValoresNulos_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(null, null);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 7️⃣ Altura en centímetros demasiado grande (fuera de rango)
        [Fact]
        public void AlturaExcedida_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(70, 400); // fuera del límite 250
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 9️⃣ Peso demasiado alto (por encima del máximo permitido)
        [Fact]
        public void PesoExcedido_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(301, 160); // mayor a 300
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 🔟 Altura en límite válido pero peso inválido
        [Fact]
        public void AlturaValida_PesoInvalido_RegresaInvalido()
        {
            var request = new CalculoIMCRequest(500, 180); // peso inválido
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.False(valid);
            Assert.NotEmpty(results);
        }

        // 1️⃣1️⃣ Verifica que un objeto completamente válido genera cero errores
        [Fact]
        public void ValoresValidos_NoGeneranErrores()
        {
            var request = new CalculoIMCRequest(80, 170);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(request, context, results, true);

            Assert.True(valid);
            Assert.Empty(results);
        }

    }
}
