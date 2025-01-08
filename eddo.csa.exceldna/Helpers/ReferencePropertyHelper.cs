using System.Linq.Expressions;
using System.Reflection;

namespace eddo.csa.exceldna.Helpers
{
    internal class ReferencePropertyHelper
    {
        public static IReadOnlyList<PropertyInfo> GetReferencedProperties<T, U>( Expression<Func<T, U>> expression )
        {
            var visitor = new ReferencedPropertyFinder( typeof( T ) );

            visitor.Visit( expression );

            return visitor.Properties;
        }
    }
}
