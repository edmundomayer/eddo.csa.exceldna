using eddo.csa.exceldna.hosting.Interfaces;
using ExcelDna.Registration;

namespace eddo.csa.exceldna.hosting
{
    internal class ExcelFunctionsProcessor : IExcelFunctionsProcessor
    {
        #region Internals
        private readonly Func<IEnumerable<ExcelFunctionRegistration>, IEnumerable<ExcelFunctionRegistration>> _process;
        #endregion Internals


        #region Constructors & Destructors
        public ExcelFunctionsProcessor( Func<IEnumerable<ExcelFunctionRegistration>, IEnumerable<ExcelFunctionRegistration>> process ) => _process = process;
        #endregion Constructors & Destructors


        #region Implements Interface IExcelFunctionsProcessor
        public IEnumerable<ExcelFunctionRegistration> Process( IEnumerable<ExcelFunctionRegistration> registrations ) => _process( registrations );
        #endregion Implements Interface IExcelFunctionsProcessor
    }
}
