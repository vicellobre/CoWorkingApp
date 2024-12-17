using CoWorkingApp.Presentation.DTOs.Reservations;

namespace CoWorkingApp.Tests.Application.DTOs
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase ReservationRequest.
    /// </summary>
    public class ReservationRequestTest
    {
        /// <summary>
        /// Prueba unitaria que verifica la inicialización de las propiedades de ReservationRequest.
        /// </summary>
        [Fact]
        public void ReservationRequestConstructor_Returns_CorrectValues_When_InitializeProperties()
        {
            // ARRANGE: Prepara el entorno de prueba creando una instancia de ReservationRequest con datos aleatorios
            var reservationRequest = new ReservationRequest
            {
                Date = DateTime.Now, // Establece la fecha actual como la fecha de reserva
                UserId = Guid.NewGuid(), // Genera un nuevo identificador único para el usuario
                SeatId = Guid.NewGuid(), // Genera un nuevo identificador único para el asiento
            };

            // ACT & ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(reservationRequest); // Verifica que la instancia de ReservationRequest no sea nula
            Assert.Equal(DateTime.Now.Date, reservationRequest.Date.Date); // Verifica que la fecha de reserva sea igual a la fecha actual
        }
    }
}
