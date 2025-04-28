// CalculoIMCsControllerTests.cs
using IMCAPI.Controllers;
using IMCAPI.Data;
using IMCAPI.Models;
using IMCAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

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
        public async Task Calcular_ConDatosValidos_RetornaOkConResultados()
        {
            // Arrange
            var request = new CalculoIMCRequest(70, 175);
            var expectedIMC = 22.86;
            var expectedCategoria = "Normal";

            _mockCalculadora.Setup(c => c.CalcularIMC(70, 175, false))
                .Returns((expectedIMC, expectedCategoria));

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;
            Assert.Equal(expectedIMC, response.imc);
            Assert.Equal(expectedCategoria, response.categoria);

            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Calcular_ConModeloInvalido_RetornaBadRequest()
        {
            // Arrange
            var request = new CalculoIMCRequest(-1, 175); // Peso inválido
            _controller.ModelState.AddModelError("Peso", "El peso debe ser positivo");

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Calcular_ConExcepcionEnCalculadora_RetornaBadRequest()
        {
            // Arrange
            var request = new CalculoIMCRequest(70, 0); // Altura cero
            _mockCalculadora.Setup(c => c.CalcularIMC(70, 0, false))
                .Throws(new ArgumentException("Altura debe ser positiva"));

            // Act
            var result = await _controller.Calcular(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Altura debe ser positiva", badRequestResult.Value);
        }

        [Fact]
        public async Task GetHistorial_RetornaListaDeCalculos()
        {
            // Arrange
            var data = new List<CalculoIMC>
            {
                new CalculoIMC { Id = 1, Peso = 70, Altura = 175, ResultadoIMC = 22.86, Categoria = "Normal" },
                new CalculoIMC { Id = 2, Peso = 80, Altura = 170, ResultadoIMC = 27.68, Categoria = "Sobrepeso" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CalculoIMC>>();
            mockSet.As<IAsyncEnumerable<CalculoIMC>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<CalculoIMC>(data.GetEnumerator()));

            mockSet.As<IQueryable<CalculoIMC>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<CalculoIMC>(data.Provider));

            mockSet.As<IQueryable<CalculoIMC>>()
                .Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CalculoIMC>>()
                .Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CalculoIMC>>()
                .Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.CalculosIMC).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetHistorial();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CalculoIMC>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<CalculoIMC>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }

    // Clases auxiliares para mockear IAsyncEnumerable
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return ValueTask.FromResult(_inner.MoveNext());
        }

        public T Current => _inner.Current;
    }

    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var expectedResultType = typeof(TResult).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                .GetMethod(
                    name: nameof(IQueryProvider.Execute),
                    genericParameterCount: 1,
                    types: new[] { typeof(Expression) })
                .MakeGenericMethod(expectedResultType)
                .Invoke(this, new[] { expression });

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(expectedResultType)
                .Invoke(null, new[] { executionResult });
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public TestAsyncEnumerable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
    }
}
