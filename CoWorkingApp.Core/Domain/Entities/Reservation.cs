using CoWorkingApp.Core.Application.Abstracts;

namespace CoWorkingApp.Core.Domain.Entities
{
    /// <summary>
    /// Representa una reserva en el sistema.
    /// </summary>
    public class Reservation : EntityBase
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
        /// Obtiene o establece el usuario asociado a la reserva.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador único del asiento asociado a la reserva.
        /// </summary>
        public Guid SeatId { get; set; }

        /// <summary>
        /// Obtiene o establece el asiento asociado a la reserva.
        /// </summary>
        public Seat Seat { get; set; }

        /// <summary>
        /// Sobrecarga del método Equals para comparar objetos Reservation por su identificador único.
        /// </summary>
        /// <param name="obj">El objeto a comparar con la reserva actual.</param>
        /// <returns>True si los objetos son iguales, de lo contrario, false.</returns>
        public override bool Equals(object obj)
        {
            // Verifica si el objeto proporcionado no es nulo y es del mismo tipo que Reservation
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            // Convierte el objeto a tipo Reservation
            Reservation other = (Reservation)obj;

            // Compara los identificadores únicos para determinar la igualdad
            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Obtiene un código hash para el objeto actual.
        /// El código hash se basa en el identificador único (Id) de la reserva.
        /// </summary>
        /// <returns>Un código hash para el objeto actual.</returns>
        public override int GetHashCode()
        {
            // Retorna el código hash del identificador único (Id) de la reserva
            return Id.GetHashCode();
        }
    }
}
