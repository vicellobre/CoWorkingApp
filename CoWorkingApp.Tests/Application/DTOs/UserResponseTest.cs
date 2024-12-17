using CoWorkingApp.Presentation.DTOs.Users;

namespace CoWorkingApp.Tests.Application.DTOs
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase UserResponse.
    /// </summary>
    public class UserResponseTest
    {
        /// <summary>
        /// Prueba unitaria que verifica la inicialización de las propiedades de UserResponse.
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="lastname">Apellido del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        [Theory]
        [InlineData("john", "andersson", "jhon@email.com")] // Datos de prueba para inicializar las propiedades
        [InlineData("maria", "pettersen", "mari@email.com")]
        public void UserResponseConstructor_Returns_CorrectValues_When_PropertiesInitialized(string name, string lastname, string email)
        {
            // ARRANGE & ACT: Prepara el entorno de prueba creando una instancia de UserResponse con los datos proporcionados
            var userResponse = new UserResponse
            {
                FirstName = name,
                LastName = lastname,
                Email = email,
            };

            // ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(userResponse); // Verifica que la instancia de UserResponse no sea nula

            Assert.NotEmpty(userResponse.FirstName); // Verifica que el nombre no esté vacío
            Assert.NotEmpty(userResponse.LastName); // Verifica que el apellido no esté vacío
            Assert.NotEmpty(userResponse.Email); // Verifica que el correo electrónico no esté vacío

            Assert.Equal(name, userResponse.FirstName); // Verifica que el nombre coincida con el valor proporcionado
            Assert.Equal(lastname, userResponse.LastName); // Verifica que el apellido coincida con el valor proporcionado
            Assert.Equal(email, userResponse.Email); // Verifica que el correo electrónico coincida con el valor proporcionado
        }
    }
}
