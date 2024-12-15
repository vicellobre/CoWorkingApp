using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities
{
    /// <summary>
    /// Clase de pruebas unitarias para la entidad User.
    /// </summary>
    public class UserTest
    {
        /// <summary>
        /// Prueba unitaria para el método Equals de la entidad User cuando los IDs son iguales.
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="lastname">Apellido del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        [Theory]
        [InlineData("maria", "perez", "maria@email.com", "0000")]
        [InlineData("jose", "luis", "email", "password")]
        [InlineData("Juan", "Lopez", "jlopez@email.com", "abc123")]
        public void Equals_Returns_True_When_IdenticalIds(string name, string lastname, string email, string password)
        {
            // ARRANGE
            Guid id = Guid.NewGuid();
            var user1 = new User { Id = id, Name = $"{name} 1", LastName = $"{lastname} 1", Email = $"{email} 1", Password = $"{password} 1" };
            var user2 = new User { Id = id, Name = $"{name} 2", LastName = $"{lastname} 2", Email = $"{email} 2", Password = $"{password} 2" };

            // ACT
            var result = true;
            var expect = user1.Equals(user2);

            // ASSERT
            Assert.Equal(expect, result);
        }

        /// <summary>
        /// Prueba unitaria para el método Equals de la entidad User cuando los IDs son diferentes.
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="lastname">Apellido del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        [Theory]
        [InlineData("maria", "perez", "maria@email.com", "0000")]
        [InlineData("jose", "luis", "email", "password")]
        [InlineData("Juan", "Lopez", "jlopez@email.com", "abc123")]
        public void Equals_Returns_False_When_DifferentIds(string name, string lastname, string email, string password)
        {
            // ARRANGE
            var user_1 = new User { Id = Guid.NewGuid(), Name = name, LastName = lastname, Email = email, Password = password };
            var user_2 = new User { Id = Guid.NewGuid(), Name = name, LastName = lastname, Email = email, Password = password };

            // ACT
            var result = false;
            var expect = user_1.Equals(user_2);

            // ASSERT
            Assert.Equal(expect, result);
        }

        /// <summary>
        /// Prueba unitaria para el método Equals de la entidad User cuando se compara con un usuario nulo.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_ComparedWith_NullUser()
        {
            // ARRANGE
            User user_1 = new();
            User? user_2 = null;

            // ACT
            var result = user_1.Equals(user_2);

            // ASSERT
            Assert.NotNull(user_1);
            Assert.Null(user_2);
            Assert.False(result);
        }

        /// <summary>
        /// Prueba unitaria para el método Equals de la entidad User cuando se compara con un objeto de tipo diferente.
        /// </summary>
        [Fact]
        public void Equals_Returns_False_When_ComparedWith_DifferentType()
        {
            // ARRANGE
            User user = new User();
            string otherObject = "";

            // ACT
            var result = user.Equals(otherObject);

            // ASSERT
            Assert.NotNull(user);
            Assert.NotNull(otherObject);
            Assert.False(result);
        }

        /// <summary>
        /// Verifica si el valor hash de dos objetos User que apuntan a la misma instancia es igual.
        /// </summary>
        [Fact]
        public void GetHashCode_Returns_SameValue_When_InstancesAreSame()
        {
            // ARRANGE
            var user_1 = new User();
            var user_2 = user_1;

            // ACT
            var result = user_1.GetHashCode();

            // ASSERT
            Assert.Equal(user_2.GetHashCode(), result);
        }

        /// <summary>
        /// Verifica si el valor hash de dos objetos User creados independientemente es diferente.
        /// </summary>
        [Fact]
        public void GetHashCode_Returns_DifferentValue_When_InstancesAreIndependent()
        {
            // ARRANGE
            var user_1 = new User();
            var user_2 = new User();

            // ACT
            var result = user_1.GetHashCode();

            // ASSERT
            Assert.NotEqual(user_2.GetHashCode(), result);
        }
    }
}
