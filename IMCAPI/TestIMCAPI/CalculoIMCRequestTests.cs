// CalculoIMCRequestTests.cs
using IMCAPI.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace IMCAPI.Tests.Models
{
    public class CalculoIMCRequestTests
    {
        [Theory]
        [InlineData(20, 50, true)] // Mínimos válidos
        [InlineData(300, 250, true)] // Máximos válidos
        [InlineData(70, 175, true)] // Valores intermedios válidos
        [InlineData(19, 50, false)] // Peso debajo del mínimo
        [InlineData(301, 250, false)] // Peso arriba del máximo
        [InlineData(70, 49, false)] // Altura debajo del mínimo
        [InlineData(70, 251, false)] // Altura arriba del máximo
        public void Validaciones_DevuelveResultadoEsperado(double peso, double alturaCm, bool esperadoValido)
        {
            // Arrange
            var request = new CalculoIMCRequest(peso, alturaCm);
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(request, context, results, true);

            // Assert
            Assert.Equal(esperadoValido, isValid);
            if (!esperadoValido)
            {
                Assert.NotEmpty(results);
            }
        }
    }
}
