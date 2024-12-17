using System.Collections;

namespace CoWorkingApp.Tests.ClassData
{
    public class ReservationRequestClassData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [DateTime.Today],
            [DateTime.Now],
            [DateTime.Now.AddDays(1)]
        ];

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
