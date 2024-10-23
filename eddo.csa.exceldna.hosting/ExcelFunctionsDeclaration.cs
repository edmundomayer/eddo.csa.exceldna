using eddo.csa.exceldna.hosting.Interfaces;

namespace eddo.csa.exceldna.hosting
{
    internal class ExcelFunctionsDeclaration<T> : IExcelFunctionsDeclaration
    {
        #region Properties
        public Type ExcelFunctionsType { get; } = typeof( T );
        #endregion Properties
    }
}
