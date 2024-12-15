using AutoMapper;
using CoWorkingApp.Application.Contracts.Adapters;
using CoWorkingApp.Infrastructure.Adapters;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.Adapters
{
    public class AutoMapperAdapterTest
    {
        public class SourceType { }
        public class DestinationType { }

        /// <summary>
        /// Verifica que el constructor de AutoMapperAdapter se ejecute correctamente cuando el IMapper no es nulo.
        /// </summary>
        [Fact]
        public void Constructor_Returns_InstanceOfAutoMapperAdapter_When_MapperIsNotNull()
        {
            // ARRANGE
            // Crea un simulacro de IMapper
            var mockMapper = new Mock<IMapper>();

            // ACT
            // Crea una instancia de AutoMapperAdapter con el IMapper simulado
            var result = new AutoMapperAdapter(mockMapper.Object);

            // ASSERT
            // Verifica que el resultado no sea nulo
            Assert.NotNull(result);
            // Verifica que el resultado sea compatible con IMapperAdapter
            Assert.IsAssignableFrom<IMapperAdapter>(result);
            // Verifica que el resultado sea una instancia de AutoMapperAdapter
            Assert.IsType<AutoMapperAdapter>(result);
        }

        /// <summary>
        /// Verifica que el constructor de AutoMapperAdapter genere una excepción de ArgumentNullException cuando se le pasa un IMapper nulo.
        /// </summary>
        [Fact]
        public void Constructor_Throws_ArgumentNullException_When_IMapperIsNull()
        {
            // ARRANGE
            // Se establece IMapper como nulo
            IMapper? mapper = null;

            // ACT
            // Se intenta crear una instancia de AutoMapperAdapter con IMapper nulo
            var result = () => new AutoMapperAdapter(mapper);

            // ASSERT
            // Verifica que se genere una excepción de ArgumentNullException
            Assert.Throws<ArgumentNullException>(result);
        }

        /// <summary>
        /// Verifica que el método Map de AutoMapperAdapter mapee correctamente entre tipos de origen y destino genéricos.
        /// </summary>
        [Fact]
        public void Map_GenericSourceAndDestination_ShouldMapCorrectly()
        {
            // ARRANGE
            // Se crea un mock de IMapper
            var mockMapper = new Mock<IMapper>();
            // Se crea una instancia de AutoMapperAdapter con el mock de IMapper
            var adapter = new AutoMapperAdapter(mockMapper.Object);
            // Se crea una instancia de tipo de origen y una instancia de tipo de destino esperada
            var source = new SourceType();
            var expectedDestination = new DestinationType();
            // Se configura el mock de IMapper para que devuelva la instancia de tipo de destino esperada al mapear desde el tipo de origen
            mockMapper.Setup(m => m.Map<SourceType, DestinationType>(source)).Returns(expectedDestination);

            // ACT
            // Se llama al método Map del adaptador con el tipo de origen
            var result = adapter.Map<SourceType, DestinationType>(source);

            // ASSERT
            // Se verifica que el resultado del mapeo sea igual a la instancia de tipo de destino esperada
            Assert.Equal(expectedDestination, result);
        }

        /// <summary>
        /// Verifica que el método Map de AutoMapperAdapter mapee correctamente entre enumerables de tipos de origen y destino.
        /// </summary>
        [Fact]
        public void Map_EnumerableSourceAndDestination_ShouldMapCorrectly()
        {
            // ARRANGE
            // Se crea una lista de tipos de origen y una lista de tipos de destino esperadas
            var sourceList = new List<SourceType>();
            var expectedDestinationList = new List<DestinationType>();

            // Se crea un mock de IMapper
            var mockMapper = new Mock<IMapper>();
            // Se crea una instancia de AutoMapperAdapter con el mock de IMapper
            var adapter = new AutoMapperAdapter(mockMapper.Object);

            // Se configura el mock de IMapper para que devuelva la lista de tipos de destino esperadas al mapear desde la lista de tipos de origen
            mockMapper.Setup(m => m.Map<IEnumerable<SourceType>, IEnumerable<DestinationType>>(sourceList)).Returns(expectedDestinationList);

            // ACT
            // Se llama al método Map del adaptador con la lista de tipos de origen
            var result = adapter.Map<SourceType, DestinationType>(sourceList);

            // ASSERT
            // Se verifica que el resultado del mapeo no sea nulo y que sea igual a la lista de tipos de destino esperadas
            Assert.NotNull(result);
            Assert.Equal(expectedDestinationList, result);
        }

    }
}
