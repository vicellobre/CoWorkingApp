# Proyecto de Ejemplo: CoWorkingApp - Desarrollo de una API con ASP.NET

Este proyecto educativo tiene como objetivo proporcionar una guía práctica para el desarrollo de una API utilizando ASP.NET Core y aplicando metodologías y buenas prácticas solicitadas en el mercado laboral. Se centra en la implementación de una arquitectura de software limpia, siguiendo principios de DDD (Domain Driven Design) y TDD (Test Driven Development), así como patrones de diseño y principios SOLID.

## Objetivo del Proyecto

El propósito principal de este proyecto es brindar a los desarrolladores una referencia práctica para comprender y aplicar conceptos clave en el desarrollo de APIs con ASP.NET Core. Se enfoca en proporcionar orientación sobre la estructura del proyecto, la organización de las capas, la implementación de casos de uso, servicios, controladores y la integración de las mejores prácticas de desarrollo.

## Contexto del Proyecto

El sistema de coworking es una plataforma diseñada para gestionar los espacios de trabajo compartidos, donde los usuarios pueden reservar asientos para trabajar temporalmente. Los principales componentes del sistema son:

- **User (Usuario)**: Los usuarios son individuos que utilizan el espacio de coworking para trabajar. Cada usuario tiene información personal, como nombre, dirección de correo electrónico y detalles de contacto.
- **Seat (Asiento)**: Los asientos representan los lugares físicos disponibles en el espacio de coworking. Cada asiento tiene un identificador único, una ubicación física y puede estar asociado con un usuario que lo haya reservado.
- **Reservation (Reserva)**: Las reservas son solicitudes realizadas por los usuarios para reservar un asiento en un momento específico. Cada reserva tiene una fecha y hora de inicio, así como una referencia al usuario que realizó la reserva y al asiento reservado.

## Características Principales

- Implementación de una arquitectura de software limpia y modular.
- Utilización de metodologías DDD y TDD para el diseño y desarrollo.
- Integración de principios SOLID, patrones de diseño y principios RESTful.
- Uso de tecnologías y herramientas como ASP.NET Core, EntityFramework, xUnit, Moq, Swagger y JWT.
- Comentarios y documentación en español para facilitar el aprendizaje de habla hispana.

## Público Objetivo

Este proyecto está dirigido a desarrolladores que deseen aprender a desarrollar APIs con ASP.NET Core, así como a aquellos interesados en comprender y aplicar metodologías de desarrollo de software, principios de diseño limpio y buenas prácticas de codificación.

## Requisitos Previos

- Conocimientos básicos de programación y desarrollo web.
- Configuración de una base de datos SQL Server local para ejecutar el proyecto.

## Instalación y Ejecución

1. Clona el repositorio desde GitHub.
2. Configura una base de datos SQL Server local y actualiza la cadena de conexión en la configuración del proyecto si es necesario.
3. Abre el proyecto en tu entorno de desarrollo preferido (Visual Studio, Visual Studio Code, etc.).
4. Compila y ejecuta la solución.

## Estructura del Proyecto

El proyecto está organizado en las siguientes capas y componentes principales:

### Core

- **Domain**: Define los objetos de dominio y los DTOs (Data Transfer Objects) utilizados para representar la información en diferentes capas de la aplicación.
  - **DTOs**: Contiene los objetos de transferencia de datos que se utilizan para la comunicación entre las diferentes capas de la aplicación.
  - **Entities**: Define las entidades del dominio que representan los conceptos fundamentales del sistema, como User, Seat y Reservation.
- **Application**: Contiene la lógica de aplicación y los casos de uso del sistema.
  - **Abstracts**: Define interfaces y abstracciones para los componentes de la aplicación.
  - **Contracts**: Define los contratos y las interfaces para los adaptadores, repositorios, solicitudes y servicios de la aplicación.
    - **Adapters**: Contiene adaptadores para integrar con sistemas externos o servicios.
    - **Repositories**: Define interfaces para los repositorios que gestionan el acceso a datos.
    - **Requests**: Define las solicitudes de datos utilizadas para transferir información entre las diferentes capas de la aplicación.
    - **Services**: Contiene las interfaces y los servicios de aplicación que encapsulan la lógica de negocio y coordinan las operaciones del sistema.
  - **Services**: Implementa los casos de uso y la lógica de negocio de la aplicación.

### Infrastructure

- **Adapters**: Implementa los adaptadores para integrar con sistemas externos o servicios.
- **Context**: Contiene el contexto de la base de datos y la configuración de Entity Framework.
- **Extensions**: Proporciona extensiones útiles para las clases y componentes de la aplicación.
- **Persistence**: Gestiona el acceso a datos y la persistencia de la información.
  - **Repositories**: Implementa los repositorios que gestionan el acceso a datos utilizando Entity Framework.
- **Presentation**: Contiene los controladores de la API que gestionan las solicitudes HTTP y retornan las respuestas correspondientes.
  - **Controllers**: Define los controladores de la API que exponen los puntos de acceso a la funcionalidad del sistema.
- **UnitOfWork**: Implementa el patrón Unit of Work para coordinar las transacciones y el acceso a datos.

## Despliegue en Azure

La API está desplegada en Azure, conectada a una base de datos SQL Server gratuita. Esto permite probar la funcionalidad de la API en un entorno de producción real.

- **API URL**: [https://coworkingapp-api-d0h6cmefabaudrgx.eastus2-01.azurewebsites.net](https://coworkingapp-api-d0h6cmefabaudrgx.eastus2-01.azurewebsites.net)

## Docker

El proyecto contiene un archivo `DockerFile` en la raíz, lo que permite probar la API utilizando una base de datos SQL Server en un contenedor Docker sin necesidad de instalar nada adicional. Esta configuración simplifica la puesta en marcha del entorno de desarrollo y asegura la consistencia entre diferentes entornos.

## Documentación

### Swagger Documentation

La documentación de la API generada automáticamente con Swagger está disponible en:

[Swagger Documentation](https://coworkingapp-api-d0h6cmefabaudrgx.eastus2-01.azurewebsites.net/swagger/index.html)

### Postman Documentation

Puedes encontrar la documentación de la colección de Postman para esta API en el siguiente enlace:

[Postman Documentation](https://www.postman.com/security-astronaut-59724338/coworking/documentation/edqfgq0/coworkingapp-api?workspaceId=dd97358b-9dc6-4838-ac9e-109e6948aee8)

### Variables de Entorno para Postman

Para probar la colección de Postman, asegúrate de configurar las siguientes variables de entorno. Puedes importar las variables desde el siguiente enlace:

[Variables de Entorno Postman](https://www.postman.com/security-astronaut-59724338/workspace/coworking/environment/19677587-0a2cdcd9-ebc1-4dfd-8960-7c99cfcd2b05?action=share&creator=19677587&active-environment=19677587-0a2cdcd9-ebc1-4dfd-8960-7c99cfcd2b05)

### Reporte de Cobertura

El reporte de cobertura para el proyecto está disponible en el siguiente enlace:

[Ver Reporte de Cobertura](https://vicellobre.github.io/CoWorkingApp/)

## Contribución

¡Todas las contribuciones son bienvenidas! Si deseas contribuir al proyecto, por favor sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama para tu función o corrección de bug.
3. Realiza tus cambios y asegúrate de que las pruebas pasen correctamente.
4. Envía un pull request con tus cambios.

## Reporte de Errores y Solicitud de Funcionalidades

Si encuentras algún error o tienes una idea para una nueva característica, por favor crea un issue en el repositorio. Estaremos encantados de revisarlo y trabajar en su resolución.

## Nota del Autor

Este es mi primer proyecto con ASP.NET Core y he intentado aplicar arquitecturas modernas de la mejor manera posible. Espero que sirva de apoyo a otros desarrolladores en su camino de aprendizaje y aplicación de buenas prácticas en el desarrollo de software.
