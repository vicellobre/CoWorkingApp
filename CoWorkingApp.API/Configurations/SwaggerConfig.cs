﻿namespace CoWorkingApp.API.Configurations
{
    /// <summary>
    /// Clase que representa la configuración de Swagger obtenida desde el archivo de configuración.
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Versión de la API que se mostrará en Swagger.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Título de la API que se mostrará en Swagger.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Descripción de la API que se mostrará en Swagger.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Información de contacto que se mostrará en la documentación de Swagger.
        /// </summary>
        public required ContactConfig Contact { get; set; }

        /// <summary>
        /// Información sobre la licencia que se mostrará en Swagger.
        /// </summary>
        public required LicenseConfig License { get; set; }

        /// <summary>
        /// Clase que representa la configuración de contacto para Swagger.
        /// </summary>
        public class ContactConfig
        {
            /// <summary>
            /// Nombre de la persona o entidad de contacto.
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// Correo electrónico de la persona o entidad de contacto.
            /// </summary>
            public string? Email { get; set; }

            /// <summary>
            /// URL del sitio web o perfil de la persona o entidad de contacto.
            /// </summary>
            public string? Url { get; set; }
        }

        /// <summary>
        /// Clase que representa la configuración de licencia para Swagger.
        /// </summary>
        public class LicenseConfig
        {
            /// <summary>
            /// Nombre de la licencia de la API.
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// URL donde se puede encontrar información sobre la licencia.
            /// </summary>
            public string? Url { get; set; }
        }
    }

}
