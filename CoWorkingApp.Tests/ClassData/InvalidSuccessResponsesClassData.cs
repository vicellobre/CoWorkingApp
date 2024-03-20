using System.Collections;

namespace CoWorkingApp.Tests.ClassData
{
    public class InvalidSuccessResponsesClassData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            new object[] { false, true, true },
            new object[] { true, false, true },
            new object[] { true, true, false },
            new object[] { false, false, true },
            new object[] { false, true, false},
            new object[] { true, false, false},
            new object[] { false, false, false },
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
