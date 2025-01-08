using System.Linq.Expressions;
using System.Reflection;

namespace eddo.csa.exceldna.Helpers
{
    sealed class ReferencedPropertyFinder : ExpressionVisitor
    {
        #region Fields
        private readonly Type _ownerType;
        private readonly List<PropertyInfo> _properties = new List<PropertyInfo>();
        #endregion Fields


        #region Constructors & Destructors
        public ReferencedPropertyFinder( Type ownerType )
        {
            _ownerType = ownerType;
        }
        #endregion Constructors & Destructors


        #region Properties
        public IReadOnlyList<PropertyInfo> Properties
        {
            get { return _properties; }
        }
        #endregion Properties


        #region Implements Abstract Class ExpressionVisitor
        protected override Expression VisitMember( MemberExpression node )
        {
            var propertyInfo = node.Member as PropertyInfo;

            //if( propertyInfo != null && RecursiveIsAssignableFrom( _ownerType, propertyInfo ) )  // _ownerType.IsAssignableFrom( propertyInfo.DeclaringType ) )
            if( propertyInfo != null )  // _ownerType.IsAssignableFrom( propertyInfo.DeclaringType ) )
            {
                _properties.Add( propertyInfo );
            }

            return base.VisitMember( node );
        }

        //private bool RecursiveIsAssignableFrom( Type type, PropertyInfo propertyInfo )
        //{
        //    bool result = type.IsAssignableFrom( propertyInfo.DeclaringType );

        //    if( !result && type.BaseType != null && type != typeof( object ) )
        //        result = result || RecursiveIsAssignableFrom( type.BaseType, propertyInfo );

        //    return result;
        //}
        #endregion Implements Abstract Class ExpressionVisitor
    }
}
