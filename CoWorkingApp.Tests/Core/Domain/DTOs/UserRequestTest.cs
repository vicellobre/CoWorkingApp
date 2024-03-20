using CoWorkingApp.Core.Domain.DTOs; // Importa el espacio de nombres donde se encuentra la clase UserRequest

namespace CoWorkingApp.Tests.Core.Domain.DTOs
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase UserRequest.
    /// </summary>
    public class UserRequestTest
    {
        /// <summary>
        /// Prueba unitaria que verifica la inicialización de las propiedades de UserRequest.
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="lastname">Apellido del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        [Theory]
        [InlineData("john", "andersson", "jhon@email.com", "1234")] // Datos de prueba para inicializar las propiedades
        [InlineData("maria", "pettersen", "mari@email.com", "000")]
        public void UserRequestConstructor_Returns_CorrectValues_When_PropertiesInitialized(string name, string lastname, string email, string password)
        {
            // ARRANGE & ACT: Prepara el entorno de prueba creando una instancia de UserRequest con los datos proporcionados
            var userRequest = new UserRequest
            {
                Name = name,
                LastName = lastname,
                Email = email,
                Password = password
            };

            // ASSERT: Realiza las acciones de prueba y verifica los resultados
            Assert.NotNull(userRequest); // Verifica que la instancia de UserRequest no sea nula

            Assert.NotEmpty(userRequest.Name); // Verifica que el nombre no esté vacío
            Assert.NotEmpty(userRequest.LastName); // Verifica que el apellido no esté vacío
            Assert.NotEmpty(userRequest.Email); // Verifica que el correo electrónico no esté vacío
            Assert.NotEmpty(userRequest.Password); // Verifica que la contraseña no esté vacía

            Assert.Equal(name, userRequest.Name); // Verifica que el nombre coincida con el valor proporcionado
            Assert.Equal(lastname, userRequest.LastName); // Verifica que el apellido coincida con el valor proporcionado
            Assert.Equal(email, userRequest.Email); // Verifica que el correo electrónico coincida con el valor proporcionado
            Assert.Equal(password, userRequest.Password); // Verifica que la contraseña coincida con el valor proporcionado
        }
    }
}
