using CoWorkingApp.Core.Application.Contracts.Requests;

namespace CoWorkingApp.Core.Domain.DTOs
{
    /// <summary>
    /// Representa la solicitud de una reserva en el sistema.
    /// </summary>
    public class ReservationRequest : IRequest
    {
        /// <summary>
        /// Obtiene o establece la fecha de la reserva.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador único del usuario asociado a la reserva.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador único del asiento asociado a la reserva.
        /// </summary>
        public Guid SeatId { get; set; }
    }
}
