using System.Collections;

namespace CoWorkingApp.Tests.ClassData
{
    /// <summary>
    /// Clase que proporciona datos de reserva no válidos para las pruebas.
    /// </summary>
    public class InvalidReservationRequestClassData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            new object[] { DateTime.Today.AddDays(-1) },
            new object[] { DateTime.Today.AddMinutes(-1) },
            new object[] { DateTime.Today.AddHours(-1) },
            new object[] { DateTime.Now.AddDays(-1) },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
