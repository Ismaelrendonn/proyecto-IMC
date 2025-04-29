// CalculoIMCRequestTests.cs
using IMCAPI.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace IMCAPI.Tests.Models
{
    public class CalculoIMCRequestTests
    {
        [Theory]
        [InlineData(20, 50, true)] 
        [InlineData(300, 250, true)] 
        [InlineData(70, 175, true)] 
        [InlineData(19, 50, false)] 
        [InlineData(301, 250, false)]
        [InlineData(70, 49, false)] 
        [InlineData(70, 251, false)] 
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
