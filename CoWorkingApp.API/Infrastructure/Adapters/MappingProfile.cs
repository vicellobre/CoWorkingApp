using AutoMapper;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;

namespace CoWorkingApp.API.Infrastructure.Adapters
{
    /// <summary>
    /// Clase que define el perfil de mapeo entre entidades del dominio y DTO para la API.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor que configura el perfil de mapeo.
        /// </summary>
        public MappingProfile()
        {
            // Configura el mapeo entre la entidad User y su correspondiente DTO UserResponse
            CreateMap<User, UserResponse>();
            // Configura el mapeo entre el DTO UserRequest y su correspondiente entidad User
            CreateMap<UserRequest, User>();

            // Configura el mapeo entre la entidad Seat y su correspondiente DTO SeatResponse
            CreateMap<Seat, SeatResponse>();
            // Configura el mapeo entre el DTO SeatRequest y su correspondiente entidad Seat
            CreateMap<SeatRequest, Seat>();

            // Agrega otros mapeos necesarios aquí
        }
    }
}
