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

            // Configura el mapeo entre la entidad Reservation y su correspondiente DTO ReservationResponse
            CreateMap<Reservation, ReservationResponse>()
                // Mapea la fecha de la reserva
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                // Mapea el nombre del usuario
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                // Mapea el apellido del usuario
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
                // Mapea el correo electrónico del usuario
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                // Mapea el nombre del asiento
                .ForMember(dest => dest.SeatName, opt => opt.MapFrom(src => src.Seat.Name))
                // Mapea la descripción del asiento
                .ForMember(dest => dest.SeatDescription, opt => opt.MapFrom(src => src.Seat.Description));

            // Configura el mapeo entre el DTO ReservationRequest y su correspondiente entidad Reservation
            CreateMap<ReservationRequest, Reservation>()
               // Mapea la fecha de la reserva
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
               // Mapea el identificador único del usuario
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               // Mapea el identificador único del asiento
               .ForMember(dest => dest.SeatId, opt => opt.MapFrom(src => src.SeatId));

            // Agrega otros mapeos necesarios aquí
        }
    }
}
