using CoWorkingApp.Core.Domain.DTOs; // Importa el espacio de nombres donde se encuentra la clase ReservationResponse

namespace CoWorkingApp.Tests.Core.Domain.DTOs
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase ReservationResponse.
    /// </summary>
    public class ReservationResponseTest
    {
        /// <summary>
        /// Prueba unitaria que verifica la inicialización de las propiedades de ReservationResponse.
        /// </summary>
        /// <param name="userName">Nombre del usuario.</param>
        /// <param name="userLastname">Apellido del usuario.</param>
        /// <param name="userEmail">Correo electrónico del usuario.</param>
        /// <param name="seatName">Nombre del asiento.</param>
        /// <param name="seatDescription">Descripción del asiento.</param>
        [Theory]
        [InlineData("john", "andersson", "jhon@email.com", "silla-1", "Marca AAA")] // Datos de prueba para inicializar las propiedades
        [InlineData("maria", "pettersen", "mari@email.com", "sofa-1", "")]
        [InlineData("Jose", "Jefferson", "jj@email.com", "sofa-2", null)]
        public void ReservationResponseConstructor_Returns_CorrectValues_When_PropertiesInitialized(string userName, string userLastname, string userEmail, string seatName, string? seatDescription)
        {
            // ARRANGE: Prepara el entorno de prueba creando una instancia de ReservationResponse con los datos proporcionados
            var reservationResponse = new ReservationResponse
            {
                Date = DateTime.Now,
                UserName = userName,
                UserLastName = userLastname,
                UserEmail = userEmail,
                SeatName = seatName,
                SeatDescription = seatDescription,
            };

            // ACT & ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(reservationResponse); // Verifica que la instancia de ReservationResponse no sea nula

            Assert.Equal(DateTime.Now.Date, reservationResponse.Date.Date); // Verifica que la fecha coincida con el valor proporcionado

            Assert.NotEmpty(reservationResponse.UserName); // Verifica que el nombre de usuario no esté vacío
            Assert.Equal(userName, reservationResponse.UserName); // Verifica que el nombre de usuario coincida con el valor proporcionado

            Assert.NotEmpty(reservationResponse.UserLastName); // Verifica que el apellido de usuario no esté vacío
            Assert.Equal(userLastname, reservationResponse.UserLastName); // Verifica que el apellido de usuario coincida con el valor proporcionado

            Assert.NotEmpty(reservationResponse.UserEmail); // Verifica que el correo electrónico de usuario no esté vacío
            Assert.Equal(userEmail, reservationResponse.UserEmail); // Verifica que el correo electrónico de usuario coincida con el valor proporcionado

            Assert.NotEmpty(reservationResponse.SeatName); // Verifica que el nombre de asiento no esté vacío
            Assert.Equal(seatName, reservationResponse.SeatName); // Verifica que el nombre de asiento coincida con el valor proporcionado
            Assert.Equal(seatDescription, reservationResponse.SeatDescription); // Verifica que la descripción de asiento coincida con el valor proporcionado
        }
    }
}
