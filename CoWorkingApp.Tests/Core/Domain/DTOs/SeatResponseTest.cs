using CoWorkingApp.Core.Domain.DTOs; // Importa el espacio de nombres donde se encuentra la clase SeatResponse

namespace CoWorkingApp.Tests.Core.Domain.DTOs
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase SeatResponse.
    /// </summary>
    public class SeatResponseTest
    {
        /// <summary>
        /// Prueba unitaria que verifica la inicialización de las propiedades de SeatResponse.
        /// </summary>
        /// <param name="name">Nombre del asiento.</param>
        /// <param name="isBlocked">Indica si el asiento está bloqueado.</param>
        /// <param name="description">Descripción del asiento.</param>
        [Theory]
        [InlineData("sofa-1", true, "sofá de lana")] // Datos de prueba para inicializar las propiedades
        [InlineData("sofa-2", false, "sofá de lino")]
        [InlineData("sofa-3", true, "")]
        [InlineData("silla-1", true, null)]
        public void SeatResponseConstructor_Returns_CorrectValues_When_InitializeProperties(string name, bool isBlocked, string? description)
        {
            // ARRANGE: Prepara el entorno de prueba creando una instancia de SeatResponse con los datos proporcionados
            var seatResponse = new SeatResponse
            {
                Name = name,
                IsBlocked = isBlocked,
                Description = description
            };

            // ACT & ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(seatResponse); // Verifica que la instancia de SeatResponse no sea nula

            Assert.NotEmpty(seatResponse.Name); // Verifica que el nombre no esté vacío

            Assert.Equal(name, seatResponse.Name); // Verifica que el nombre coincida con el valor proporcionado
            Assert.Equal(isBlocked, seatResponse.IsBlocked); // Verifica que el estado de bloqueo coincida con el valor proporcionado
            Assert.Equal(description, seatResponse.Description); // Verifica que la descripción coincida con el valor proporcionado
        }
    }
}
