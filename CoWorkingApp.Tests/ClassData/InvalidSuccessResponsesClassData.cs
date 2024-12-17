using System.Collections;

namespace CoWorkingApp.Tests.ClassData
{
    public class InvalidSuccessResponsesClassData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [false, true, true],
            [true, false, true],
            [true, true, false],
            [false, false, true],
            [false, true, false],
            [true, false, false],
            [false, false, false],
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
