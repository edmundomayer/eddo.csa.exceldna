using ExcelDna.Registration;

namespace eddo.csa.exceldna.hosting.Interfaces
{
    public interface IExcelFunctionsProcessor
    {
        IEnumerable<ExcelFunctionRegistration> Process(IEnumerable<ExcelFunctionRegistration> registrations);
    }
}
