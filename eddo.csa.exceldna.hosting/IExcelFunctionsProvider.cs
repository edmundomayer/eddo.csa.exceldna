using ExcelDna.Registration;

namespace eddo.csa.exceldna.hosting
{
    internal interface IExcelFunctionsProvider
    {
        IEnumerable<ExcelFunctionRegistration> GetExcelFunctions();
    }
}
