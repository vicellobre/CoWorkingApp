using CoWorkingApp.Core.Domain.Entities; // Importa el espacio de nombres donde se encuentra la clase Seat

namespace CoWorkingApp.Tests.Core.Domain.Entities
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase Seat.
    /// </summary>
    public class SeatTest
    {
        /// <summary>
        /// Prueba unitaria que verifica las propiedades de un asiento.
        /// </summary>
        /// <param name="name">El nombre del asiento.</param>
        /// <param name="isBlocked">Indica si el asiento está bloqueado.</param>
        /// <param name="description">La descripción del asiento.</param>
        [Theory]
        [InlineData("silla-1", true, "Ubicado en la sala")]
        [InlineData("banco-2", false, "Al aire libre")]
        [InlineData("sofa-1", true, "Para dos personas")]
        public void Seat_Properties_Returns_CorrectValues_When_Initialized(string name, bool isBlocked, string description)
        {
            // ARRANGE: Prepara el entorno de prueba creando una instancia de Seat y estableciendo sus propiedades
            Seat seat = new();

            // ACT: Asigna los valores de las propiedades del asiento
            seat.Name = name;
            seat.IsBlocked = isBlocked;
            seat.Description = description;

            // ASSERT: Verifica los valores de las propiedades del asiento
            Assert.NotNull(seat); // Verifica que la instancia de Seat no sea nula
            Assert.NotNull(seat.Name); // Verifica que el nombre del asiento no sea nulo
            Assert.NotEmpty(seat.Name); // Verifica que el nombre del asiento no esté vacío
            Assert.Equal(name, seat.Name); // Verifica que el nombre del asiento sea igual al nombre proporcionado

            Assert.NotNull(seat.Description); // Verifica que la descripción del asiento no sea nula
            Assert.NotEmpty(seat.Description); // Verifica que la descripción del asiento no esté vacía
            Assert.Equal(description, seat.Description); // Verifica que la descripción del asiento sea igual a la descripción proporcionada

            Assert.Equal(isBlocked, seat.IsBlocked); // Verifica si el asiento está bloqueado o no

            Assert.NotNull(seat.Reservations); // Verifica que la lista de reservas no sea nula
            Assert.Empty(seat.Reservations); // Verifica que la lista de reservas esté vacía
        }

        /// <summary>
        /// Prueba unitaria que verifica el comportamiento cuando la descripción del asiento es nula.
        /// </summary>
        /// <param name="name">El nombre del asiento.</param>
        /// <param name="isBlocked">Indica si el asiento está bloqueado.</param>
        /// <param name="description">La descripción del asiento (se espera que sea nula).</param>
        [Theory]
        [InlineData("silla-1", true, "Ubicado en la sala")]
        [InlineData("banco-2", false, "Al aire libre")]
        [InlineData("sofa-1", true, "Para dos personas")]
        public void Seat_NullDescription_Returns_Null_When_DescriptionIsNull(string name, bool isBlocked, string description)
        {
            // ARRANGE: Prepara el entorno de prueba creando una instancia de Seat y estableciendo su descripción
            var seat = new Seat { Name = name, IsBlocked = isBlocked, Description = description };

            // ACT: Establece la descripción del asiento como nula
            seat.Description = null;

            // ASSERT: Verifica que la descripción del asiento sea nula
            Assert.NotNull(seat); // Verifica que la instancia de Seat no sea nula
            Assert.Null(seat.Description); // Verifica que la descripción del asiento sea nula
        }

        /// <summary>
        /// Verifica que el método Equals de la clase Seat devuelve verdadero cuando dos asientos tienen el mismo Id.
        /// </summary>
        /// <param name="name">El nombre del asiento.</param>
        /// <param name="isBlocked">Indica si el asiento está bloqueado.</param>
        /// <param name="description">La descripción del asiento.</param>
        [Theory]
        [InlineData("Q-1", true, "This seat is located near the window.")]
        [InlineData("M-2", false, "email")]
        public void Equals_Returns_True_When_IdsAreEqual(string name, bool isBlocked, string description)
        {
            // ARRANGE
            // Se genera un Guid para el Id del asiento
            Guid id = Guid.NewGuid();
            // Se crean dos instancias de Seat con el mismo Id pero diferentes valores de otros atributos
            var seat_1 = new Seat { Id = id, Name = name, IsBlocked = isBlocked, Description = description };
            var seat_2 = new Seat { Id = id, Name = name, IsBlocked = isBlocked, Description = description };

            // ACT
            // Se compara si los dos asientos son iguales utilizando el método Equals
            var result = true; // El resultado esperado
            var expect = seat_1.Equals(seat_2);

            // ASSERT
            // Se verifica que el resultado sea igual al esperado
            Assert.Equal(expect, result);
        }

        /// <summary>
        /// Verifica que el método Equals de la clase Seat devuelve falso cuando dos asientos tienen Id diferentes.
        /// </summary>
        /// <param name="name">El nombre del asiento.</param>
        /// <param name="isBlocked">Indica si el asiento está bloqueado.</param>
        /// <param name="description">La descripción del asiento.</param>
        [Theory]
        [InlineData("Q-1", true, "This seat is located near the window.")]
        [InlineData("M-2", false, "email")]
        public void Equals_Returns_False_When_IdsAreDifferent(string name, bool isBlocked, string description)
        {
            // ARRANGE
            // Se generan dos Guid diferentes para los Id de los asientos
            var seat_1 = new Seat { Id = Guid.NewGuid(), Name = name, IsBlocked = isBlocked, Description = description };
            var seat_2 = new Seat { Id = Guid.NewGuid(), Name = name, IsBlocked = isBlocked, Description = description };

            // ACT
            // Se compara si los dos asientos son iguales utilizando el método Equals
            var result = false; // El resultado esperado es falso ya que los Id son diferentes
            var expect = seat_1.Equals(seat_2);

            // ASSERT
            // Se verifica que el resultado sea igual al esperado
            Assert.Equal(expect, result);
        }

        /// <summary>
        /// Verifica que el método Equals de la clase Seat devuelve falso cuando se compara con un asiento nulo.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_ComparedWith_NullSeat()
        {
            // ARRANGE
            // Se crea un nuevo asiento
            Seat seat_1 = new();
            // Se asigna null a seat_2
            Seat? seat_2 = null;

            // ACT
            // Se compara el primer asiento con null utilizando el método Equals
            var result = seat_1.Equals(seat_2);

            // ASSERT
            // Se verifica que seat_1 no sea nulo, que seat_2 sea nulo y que el resultado sea falso
            Assert.NotNull(seat_1);
            Assert.Null(seat_2);
            Assert.False(result);
        }

        /// <summary>
        /// Verifica que el método Equals de la clase Seat devuelve falso cuando se compara con un objeto de un tipo diferente.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_ComparedWith_DifferentType()
        {
            // ARRANGE
            // Se crea una instancia de Seat
            Seat seat = new Seat();
            // Se crea un objeto de un tipo diferente
            string otherObject = "";

            // ACT
            // Se llama al método Equals para comparar el asiento con el otro objeto
            var result = seat.Equals(otherObject);

            // ASSERT
            // Se verifica que ambos objetos no sean nulos
            Assert.NotNull(seat);
            Assert.NotNull(otherObject);
            // Se verifica que el resultado sea falso, ya que los tipos de objetos son diferentes
            Assert.False(result);
        }

        /// <summary>
        /// Verifica si el valor hash de dos objetos Seat que apuntan a la misma instancia es igual.
        /// </summary>
        [Fact]
        public void GetHashCode_Returns_SameValue_When_InstancesAreSame()
        {
            // ARRANGE
            var seat_1 = new Seat();
            var seat_2 = seat_1;

            // ACT
            var result = seat_1.GetHashCode();

            // ASSERT
            Assert.Equal(seat_2.GetHashCode(), result);
        }

        /// <summary>
        /// Verifica si el valor hash de dos objetos Seat creados independientemente es diferente.
        /// </summary>
        [Fact]
        public void GetHashCode_Returns_DifferentValue_When_InstancesAreIndependent()
        {
            // ARRANGE
            var seat_1 = new Seat();
            var seat_2 = new Seat();

            // ACT
            var result = seat_1.GetHashCode();

            // ASSERT
            Assert.NotEqual(seat_2.GetHashCode(), result);
        }
    }
}
