using ExcelDna.Registration;

namespace eddo.csa.exceldna.hosting.Interfaces
{
    internal interface IExcelFunctionsProvider
    {
        IEnumerable<ExcelFunctionRegistration> GetExcelFunctions();
    }
}
