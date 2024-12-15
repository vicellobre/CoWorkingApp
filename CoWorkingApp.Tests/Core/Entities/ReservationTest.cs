using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase Reservation.
    /// </summary>
    public class ReservationTest
    {
        /// <summary>
        /// Prueba unitaria que verifica las propiedades de una reserva.
        /// </summary>
        [Fact]
        public void ReservationConstructor_Returns_CorrectValues_When_PropertiesInitialized()
        {
            // ARRANGE & ACT: Prepara el entorno de prueba creando una instancia de Reservation con datos aleatorios
            var reservation = new Reservation
            {
                Date = DateTime.Now, // Establece la fecha actual como la fecha de la reserva
                UserId = Guid.NewGuid(), // Genera un nuevo identificador único para el usuario
                SeatId = Guid.NewGuid() // Genera un nuevo identificador único para el asiento
            };

            // Asigna los objetos User y Seat a la reserva, estableciendo sus identificadores
            reservation.User = new User { Id = reservation.UserId };
            reservation.Seat = new Seat { Id = reservation.SeatId };

            // ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(reservation); // Verifica que la instancia de Reservation no sea nula
            Assert.NotNull(reservation.User); // Verifica que la propiedad User de la reserva no sea nula
            Assert.NotNull(reservation.Seat); // Verifica que la propiedad Seat de la reserva no sea nula
            Assert.Equal(DateTime.Now.Date, reservation.Date.Date); // Verifica que la fecha de reserva sea igual a la fecha actual
        }

        /// <summary>
        /// Prueba si dos objetos Reservation son iguales en función de sus ID.
        /// </summary>
        [Fact]
        public void Equals_Returns_True_When_IdsAreEqual()
        {
            // ARRANGE
            // Generar un ID único para las reservaciones
            Guid id = Guid.NewGuid();
            // Crear dos objetos Reservation con el mismo ID
            var reservation_1 = new Reservation { Id = id };
            var reservation_2 = new Reservation { Id = id };

            // ACT
            // Comprobar si los objetos Reservation son iguales según su ID
            var result = true;
            var expect = reservation_1.Equals(reservation_2);

            // ASSERT
            // Verificar que el resultado de la comparación sea verdadero
            Assert.Equal(expect, result);
        }

        /// <summary>
        /// Prueba si dos objetos Reservation son diferentes en función de sus ID.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_IdsAreDifferent()
        {
            // ARRANGE
            // Crear dos objetos Reservation sin asignarles ID, lo que los hace diferentes
            var reservation_1 = new Reservation();
            var reservation_2 = new Reservation();

            // ACT
            // Comprobar si los objetos Reservation son iguales
            var result = false;
            var expect = reservation_1.Equals(reservation_2);

            // ASSERT
            // Verificar que el resultado de la comparación sea falso
            Assert.Equal(expect, result);
        }

        /// <summary>
        /// Prueba si un objeto Reservation es diferente de nulo.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_ComparedWith_NullReservation()
        {
            // ARRANGE
            // Crear un objeto Reservation y asignarle un valor, mientras que el otro se deja como nulo
            Reservation reservation_1 = new();
            Reservation? reservation_2 = null;

            // ACT
            // Comprobar si el objeto Reservation es igual a nulo
            var result = reservation_1.Equals(reservation_2);

            // ASSERT
            // Verificar que el primer objeto Reservation no sea nulo
            Assert.NotNull(reservation_1);
            // Verificar que el segundo objeto Reservation sea nulo
            Assert.Null(reservation_2);
            // Verificar que el resultado de la comparación sea falso
            Assert.False(result);
        }

        /// <summary>
        /// Prueba si un objeto Reservation es igual a un objeto de otro tipo.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_ComparedWith_DifferentType()
        {
            // ARRANGE
            // Crear un objeto Reservation
            Reservation reservation = new Reservation();
            // Crear un objeto de otro tipo (string en este caso)
            string otherObject = "";

            // ACT
            // Comprobar si el objeto Reservation es igual al objeto de otro tipo
            var result = reservation.Equals(otherObject);

            // ASSERT
            // Verificar que el objeto Reservation no sea nulo
            Assert.NotNull(reservation);
            // Verificar que el objeto de otro tipo no sea nulo
            Assert.NotNull(otherObject);
            // Verificar que el resultado de la comparación sea falso
            Assert.False(result);
        }

        /// <summary>
        /// Comprueba si los códigos hash de dos objetos Reservation son iguales.
        /// </summary>
        [Fact]
        public void GetHashCode_Returns_SameValue_When_InstancesAreSame()
        {
            // ARRANGE
            // Crear dos objetos Reservation
            var reservation_1 = new Reservation();
            var reservation_2 = reservation_1;

            // ACT
            // Obtener el código hash del primer objeto Reservation
            var result = reservation_1.GetHashCode();

            // ASSERT
            // Verificar que los códigos hash de ambos objetos Reservation son iguales
            Assert.Equal(reservation_2.GetHashCode(), result);
        }

        /// <summary>
        /// Comprueba si los códigos hash de dos objetos Reservation son diferentes.
        /// </summary>
        [Fact]
        public void GetHashCode_Returns_DifferentValue_When_InstancesAreIndependent()
        {
            // ARRANGE
            // Crear dos objetos Reservation diferentes
            var reservation_1 = new Reservation();
            var reservation_2 = new Reservation();

            // ACT
            // Obtener el código hash del primer objeto Reservation
            var result = reservation_1.GetHashCode();

            // ASSERT
            // Verificar que los códigos hash de ambos objetos Reservation son diferentes
            Assert.NotEqual(reservation_2.GetHashCode(), result);
        }
    }
}
