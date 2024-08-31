using System.Collections.Generic;
using System.Linq;

namespace Shun_Utilities
{
    public static class IEnumeratorExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)       
            => self.Select((item, index) => (item, index));
    }
}