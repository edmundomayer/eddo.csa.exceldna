namespace eddo.csa.exceldna.hosting
{
    internal class ExcelFunctionsDeclaration<T> : IExcelFunctionsDeclaration
    {
        public Type ExcelFunctionsType { get; } = typeof( T );
    }
}
