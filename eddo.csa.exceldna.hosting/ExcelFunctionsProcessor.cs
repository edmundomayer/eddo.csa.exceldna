using ExcelDna.Registration;

namespace eddo.csa.exceldna.hosting
{
    internal class ExcelFunctionsProcessor : IExcelFunctionsProcessor
    {
        private readonly Func<IEnumerable<ExcelFunctionRegistration>, IEnumerable<ExcelFunctionRegistration>> _process;

        public ExcelFunctionsProcessor( Func<IEnumerable<ExcelFunctionRegistration>, IEnumerable<ExcelFunctionRegistration>> process ) => _process = process;

        public IEnumerable<ExcelFunctionRegistration> Process( IEnumerable<ExcelFunctionRegistration> registrations ) => _process( registrations );
    }
}
