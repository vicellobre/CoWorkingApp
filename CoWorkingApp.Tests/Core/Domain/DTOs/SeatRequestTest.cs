using CoWorkingApp.Core.Domain.DTOs; // Importa el espacio de nombres donde se encuentra la clase SeatRequest

namespace CoWorkingApp.Tests.Core.Domain.DTOs
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase SeatRequest.
    /// </summary>
    public class SeatRequestTest
    {
        /// <summary>
        /// Prueba unitaria que verifica la inicialización de las propiedades de SeatRequest.
        /// </summary>
        /// <param name="name">Nombre del asiento.</param>
        /// <param name="isBlocked">Indica si el asiento está bloqueado.</param>
        /// <param name="description">Descripción del asiento.</param>
        [Theory]
        [InlineData("sofa-1", true, "sofá de lana")] // Datos de prueba para inicializar las propiedades
        [InlineData("sofa-2", false, "sofá de lino")]
        [InlineData("sofa-3", true, "")]
        [InlineData("silla-1", true, null)]
        public void SeatRequestConstructor_Returns_CorrectValues_When_InitializeProperties(string name, bool isBlocked, string? description)
        {
            // ARRANGE: Prepara el entorno de prueba creando una instancia de SeatRequest con los datos proporcionados
            var seatRequest = new SeatRequest
            {
                Name = name,
                IsBlocked = isBlocked,
                Description = description
            };

            // ACT & ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(seatRequest); // Verifica que la instancia de SeatRequest no sea nula

            Assert.NotEmpty(seatRequest.Name); // Verifica que el nombre no esté vacío

            Assert.Equal(name, seatRequest.Name); // Verifica que el nombre coincida con el valor proporcionado
            Assert.Equal(isBlocked, seatRequest.IsBlocked); // Verifica que el estado de bloqueo coincida con el valor proporcionado
            Assert.Equal(description, seatRequest.Description); // Verifica que la descripción coincida con el valor proporcionado
        }
    }
}
