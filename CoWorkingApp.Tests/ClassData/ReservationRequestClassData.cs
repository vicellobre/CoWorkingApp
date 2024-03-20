using System.Collections;

namespace CoWorkingApp.Tests.ClassData
{
    public class ReservationRequestClassData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            new object[] { DateTime.Today },
            new object[] { DateTime.Now },
            new object[] { DateTime.Now.AddDays(1) }
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
