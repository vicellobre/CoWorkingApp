using CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;

namespace CoWorkingApp.Presentation.Reservations.V2.Factories
{
    /// <summary>
    /// Fábrica para crear consultas de reservas.
    /// </summary>
    public static class ReservationQueryFactory
    {
        /// <summary>
        /// Crea una consulta de reservas basada en el tipo de parámetro.
        /// </summary>
        /// <typeparam name="T">El tipo del parámetro.</typeparam>
        /// <param name="parameter">El parámetro para la consulta.</param>
        /// <param name="queryType">El tipo de consulta a crear.</param>
        /// <returns>Una consulta de reservas.</returns>
        public static object Create<T>(T parameter, string queryType)
        {
            if (parameter is Guid guid)
            {
                return queryType.ToLower() switch
                {
                    "user" => new GetReservationsByUserIdQuery(guid),
                    "seat" => new GetReservationsBySeatIdQuery(guid),
                    _ => throw new ArgumentException("Tipo de consulta no soportado")
                };
            }
            
            if (parameter is DateTime dateTime)
            {
                return new GetReservationsByDateQuery(dateTime);
            }

            throw new ArgumentException("Tipo de parámetro no soportado");
        }
    }
}
